using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuccessAppService.Framework.Entity
{
    public class eCharAttri
    {
        public string caption { get; set; }
        public string xAxisName { get; set; }
        public string yAxisName { get; set; }
        public string showBorder { get; set; }
        public string paletteColors { get; set; }
        public string bgColor { get; set; }
        public string usePlotGradientColor { get; set; }
        public string showCanvasBorder { get; set; }
        public string LegendShadow { get; set; }
        public string legendBorderAlpha { get; set; }
        public string axisLineAlpha { get; set; }
        public string showAlternateHgridColor { get; set; }
        public string showValues { get; set; }
        public string baseFontColor { get; set; }
        public string captionFontSize { get; set; }
        public string subcaptionFontSize { get; set; }
        public string subcaptionFontBold { get; set; }
        public string toolTipColor { get; set; }
        public string toolTipBorderThickness { get; set; }
        public string toolTipBgAlpha { get; set; }
        public string toolTipPadding { get; set; }

        //add by ndhung 2016.12.20 -> thêm kích thước của chart
        public string xAxisNameFontSize { get; set; }
        public string yAxisNameFontSize { get; set; }
        public string valueFontSize { get; set; }
        public string baseFontSize { get; set; }
    }
}