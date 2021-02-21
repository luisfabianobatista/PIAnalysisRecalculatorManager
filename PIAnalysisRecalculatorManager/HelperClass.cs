/****************************************************************************
*                                                                           *
* The code listed is the HelperClass.cs file was extracted from the tutorial* 
*  "A faster way to get PIPoints from a large list of AFAttributes"         *
*   written by Rick Davin, Nalco Champion, March 22, 2015                   *
*                                                                           *
/****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.PI;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace PIAnalysisRecalculatorManager
{
    public static class HelperClass
    {

        public static bool UsesPIPointDR(this AFAttribute attribute)
        {
            return (attribute != null) &&
                   (attribute.DataReferencePlugIn != null) &&
                   (attribute.DataReferencePlugIn.Name == "PI Point");
        }


        public static IList<AFAttribute> GetPIPointAttributes(this IList<AFAttribute> attributes)
        {
            var hashed = new HashSet<AFAttribute>();
            if ((attributes != null) && (attributes.Count > 0))
            {
                foreach (var attribute in attributes)
                {
                    if (UsesPIPointDR(attribute))
                    {
                        if (!hashed.Contains(attribute)) hashed.Add(attribute);
                    }
                }
            }
            return hashed.ToList();
        }



        public static HashSet<AFAttribute> GetValidPIPointAttributes(this IList<AFAttribute> attributes)
        {
            var subset = GetPIPointAttributes(attributes);
            if (subset.Count == 0) return new HashSet<AFAttribute>();

            // Populate found before entering parallel loops so that
            // we can avoid some concurrency issues up-front.
            var found = subset.ToDictionary(key => key, value => false);

            var threadCount = 10;
            var chunkSize = 1000;
            var chunks = Partitioner.Create(0, subset.Count, chunkSize);

            // NOTE: OSIsoft recommends you always set MaxDegreeOfParallelism.
            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = threadCount;

            Parallel.ForEach(chunks, options, chunk =>
            {
                var inclusiveStartIndex = chunk.Item1;
                var exclusiveEndIndex = chunk.Item2;
                // Gather paths for this chunk.
                var paths = new Dictionary<AFAttribute, string>();
                for (var i = inclusiveStartIndex; i < exclusiveEndIndex; i++)
                {
                    var path = subset[i].RawPIPointPath(AFEncodeType.Name);
                    if (!string.IsNullOrWhiteSpace(path)) path = path.ToUpperInvariant();
                    paths.Add(subset[i], path);
                }
                // Query PI Server(s) - Issue bulk call for this chunk
                var queryPaths = paths.Values.Where(x => (string.IsNullOrWhiteSpace(x) == false));
                var validated = PIPoint.FindPIPointsByPath(queryPaths).Results;
                // Try to map back to found dictionary
                for (var i = inclusiveStartIndex; i < exclusiveEndIndex; i++)
                {
                    var attribute = subset[i];
                    var path = paths[attribute];
                    if (!string.IsNullOrWhiteSpace(path) && validated.ContainsKey(path))
                    {
                        // The PIPoint should be in our cache thanks to FindPIPointsByPath().
                        found[attribute] = true;
                    }
                }
            });

            // return result could be smaller than 'subset' which could be smaller than input 'attributes'.
            return new HashSet<AFAttribute>(found.Where(x => (x.Value == true)).Select(x => x.Key));
        }

        /// <summary>
        /// Converts an object collection to a DataTable.
        /// Author: Ehsan Sajjad
        /// Source: https://stackoverflow.com/questions/14477500/adding-object-to-datatable-and-create-a-dynamic-gridview
        /// Example of usage: 
        ///      List<Product> products = new List<Product>();
        ///      DataTable dtProducts = products.ToDataTable();
        /// </summary>
        /// <typeparam name="T">The type of items in the list</typeparam>
        /// <param name="iList">The collection (list) of objects to be converte to DataTable</param>
        /// <returns>The object collection converted to DataTable</returns>
        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));


            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;
             

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                //The following 2 lines was not part of the original source code extracted from stackoverflow
                dataTable.Columns.Add(propertyDescriptor.Name, type);
                dataTable.Columns[propertyDescriptor.Name].ColumnName = propertyDescriptor.DisplayName;           

                        
            }

            object[] values = new object[propertyDescriptorCollection.Count];
          
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                    
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }





    }
}
