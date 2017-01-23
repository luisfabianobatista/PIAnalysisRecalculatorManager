using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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
                return afAnalysis.Template.Name;
            }
        }

        public string Scheduling
        {
            get
            {
                return afAnalysis.TimeRule.ToString();
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

