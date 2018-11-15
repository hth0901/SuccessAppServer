using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuccessAppService.Framework.Entity
{
    public class eCharAttDoughnut
    {
        public string caption { get; set; }
        public string numberPrefix { get; set; }
        public string paletteColors { get; set; }
        public string bgColor { get; set; }
        public string showBorder { get; set; }
        public string use3DLighting { get; set; }
        public string showShadow { get; set; }
        public string enableSmartLabels { get; set; }
        public string startingAngle { get; set; }
        public string showLabels { get; set; }
        public string showPercentValues { get; set; }
        public string showLegend { get; set; }
        public string legendShadow { get; set; }
        public string legendBorderAlpha { get; set; }
        public string centerLabelBold { get; set; }
        public string showTooltip { get; set; }
        public string decimals { get; set; }
        public string captionFontSize { get; set; }
        public string subcaptionFontSize { get; set; }
        public string subcaptionFontBold { get; set; }

        public string labelFontSize { get; set; } //add by ndhung 2016.12.16 -> kích thước chữ số %
        public string baseFontSize { get; set; } //add by ndhung 2016.12.16 -> kích thước chữ số %
    }
}