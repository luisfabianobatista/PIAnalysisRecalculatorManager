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

        public string Template
        {
            get
            {
                if (afAnalysis.Template !=null)
                {
                    return afAnalysis.Template.Name;
                }

                return String.Empty;
                
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

        public string Name
        {
            get
            {
                return afAnalysis.Name;
            }
        }

        public string Target
        {
            get
            {
                return afAnalysis.Target.ToString();
            }
        }

        public OSIsoft.AF.Analysis.AFStatus Status
        {
            get

            {
                return afAnalysis.Status; //return the cached data
            }
            //set
            //{
                
            //    afAnalysis.SetStatus(Status);
            //    afAnalysis.Status = Status; //refreshing the cached data too
            //}
        }

        public AFAnalysisObj(OSIsoft.AF.Analysis.AFAnalysis analysis, bool selectedState)
        {
            afAnalysis = analysis;
            Select = selectedState;
            
        }
    }


}

