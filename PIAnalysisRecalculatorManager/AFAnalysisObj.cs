using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PIAnalysisRecalculatorManager
{
    public class AFAnalysisObj
    {
        //private OSIsoft.AF.Analysis.AFStatus _StatusCached;

        [Browsable(false)]
        public OSIsoft.AF.Analysis.AFAnalysis afAnalysis { get; set; }

        
        public bool Select { get; set; }

        public string Path
        {
            get
            {
                return afAnalysis.GetPath();
            }
        }

        [System.ComponentModel.DisplayName("Analysis")]
        public string Name
        {
            get
            {
                return afAnalysis.Name;
            }
        }

        [System.ComponentModel.DisplayName("Analysis Categories")]
        public string Categories
        {
            get
            {
                return afAnalysis.CategoriesString;
            }
        }

        public string Scheduling
        {
            get
            {
                if (afAnalysis.TimeRule != null)
                {
                    string strTimeRule = afAnalysis.TimeRule.ToString();
                    if (strTimeRule != null)
                    {
                        string outputString = Regex.Replace(afAnalysis.TimeRule.ToString(), @"\{?[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\}\;{1}?", "");
                        //remove the attribute GUIDs from the triggering info

                        return outputString.Replace(@"""", "");
                      
                    }

                    return "Any Input";

                   

                }

                return String.Empty;
                
            }
        }

              

        [System.ComponentModel.DisplayName("Element")]
        public string Target
        {
            get
            {
                return afAnalysis.Target.ToString();
            }
        }

        [System.ComponentModel.DisplayName("Element Template")]
        public string Template
        {
            get
            {

                OSIsoft.AF.Asset.AFElement analysisTarget = (OSIsoft.AF.Asset.AFElement)afAnalysis.Target;

                if (analysisTarget.Template != null)
                {
                    return analysisTarget.Template.Name;
                }

                return String.Empty;

            }
        }

        [System.ComponentModel.DisplayName("Element Categories")]
        public string ElementCategories
        {
            get
            {
              
                OSIsoft.AF.Asset.AFElement analysisTarget = (OSIsoft.AF.Asset.AFElement)afAnalysis.Target;

                if (analysisTarget.Template != null)
                {
                    return analysisTarget.CategoriesString;

                }
                                  
                return String.Empty;

            }
        }

        //public OSIsoft.AF.Analysis.AFStatus Status
        public string Status
        {
            get

            {
                return afAnalysis.Status.ToString(); //return the cached data
                
            }

        }

        public AFAnalysisObj(OSIsoft.AF.Analysis.AFAnalysis analysis, bool selectedState)
        {
            afAnalysis = analysis;
            Select = selectedState;
            
        }
    }


}

