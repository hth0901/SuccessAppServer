using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuccessAppService.Framework.Entity;
using System.Threading;
using System.Globalization;

namespace SuccessAppService.Framework.Ultility
{
    public class Util
    {
        // Author: Tuan-It (1994 )
        // Page Init: DashBoard
        // Create Chart
        public static eCharAttri CharAttr()
        {
            return new eCharAttri
            {
                caption = "Weekly Attendance",
                xAxisName = "Day",
                yAxisName = "Employee",
                showBorder = "0",
                paletteColors = "#33A5C1,#FFB360,#9BBB59",
                bgColor = "#ffffff",
                usePlotGradientColor = "0",
                showCanvasBorder = "0",
                LegendShadow = "0",
                legendBorderAlpha = "0",
                axisLineAlpha = "40",
                showAlternateHgridColor = "0",
                showValues = "0",
                baseFontColor = "#333333",
                captionFontSize = "14",
                subcaptionFontSize = "14",
                subcaptionFontBold = "0",
                toolTipColor = "#4caf50",
                toolTipBorderThickness = "0",
                toolTipBgAlpha = "80",
                toolTipPadding = "5",

                baseFontSize = "11"

            };
        }

        public static eCharAttDoughnut CharAttrDoughnut()
        {
            return new eCharAttDoughnut
            {
                caption = "Total Staffs",
                showBorder = "0",
                paletteColors = "#F45B00, #F2C500, #1AAF5D, #0075C2,#8e0000",
                bgColor = "#ffffff",
                legendBorderAlpha = "0",
                captionFontSize = "14", //kích thước tiêu đề
                subcaptionFontSize = "14",  //kích thước tiêu đề nhỏ bên dưới
                subcaptionFontBold = "0",
                centerLabelBold ="1",
                decimals = "0",
                enableSmartLabels = "0",
                legendShadow = "0",
                numberPrefix = "$",
                showLabels = "0",
                showLegend = "1",
                showPercentValues = "1",
                showShadow = "1",
                showTooltip = "1",
                startingAngle = "310",
                use3DLighting = "0",

                labelFontSize= "11", //kích thước số %
                baseFontSize = "11" //kích thước chữ bên dưới 
            };
        }

        /*
        public static List<eCategoryChar> ListELabels()
        {
            var listLabel = new List<eLabel>
            {
                new eLabel {label = LanguageHelper.GetResourcesValue("Sun")},
                new eLabel {label = LanguageHelper.GetResourcesValue("Mon")},
                new eLabel {label = LanguageHelper.GetResourcesValue("Tue")},
                new eLabel {label = LanguageHelper.GetResourcesValue("Wed")},
                new eLabel {label = LanguageHelper.GetResourcesValue("Thu")},
                new eLabel {label = LanguageHelper.GetResourcesValue("Fri")},
                new eLabel {label = LanguageHelper.GetResourcesValue("Sat")}
            };
            var category = new List<eLabel>();
            category.AddRange(listLabel);
            return new List<eCategoryChar>
            {
                new eCategoryChar
                {
                    category = category
                }
            };
        }*/
    }
}