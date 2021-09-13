using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Model.Common
{
    public static class CommonMethods
    {
        public static string EnglishMonthToBanglaMonth(int month, int year)
        {
            var banglaMonth = string.Empty;

            switch (month)
            {
                case 1:
                    banglaMonth = "জানুয়ারী";
                    break;
                case 2:
                    banglaMonth = "ফেব্রুয়ারী";
                    break;
                case 3:
                    banglaMonth = "মার্চ";
                    break;
                case 4:
                    banglaMonth = "এপ্রিল";
                    break;
                case 5:
                    banglaMonth = "মে";
                    break;
                case 6:
                    banglaMonth = "জুন";
                    break;
                case 7:
                    banglaMonth = "জুলাই";
                    break;
                case 8:
                    banglaMonth = "আগস্ট";
                    break;
                case 9:
                    banglaMonth = "সেপ্টেম্বর";
                    break;
                case 10:
                    banglaMonth = "অক্টোবর";
                    break;
                case 11:
                    banglaMonth = "নভেম্বর";
                    break;
                case 12:
                    banglaMonth = "ডিসেম্বর ";
                    break;

                default:
                    break;
            }

            banglaMonth = banglaMonth + "'" + EnglishYearToBanglaYear(year);
            return banglaMonth;
        }

        public static string EnglishYearToBanglaYear(int year)
        {
            string cyear = year.ToString().Substring(year.ToString().Length - 2);

            return string.Concat(cyear.ToString().Select(c => (char)('\u09E6' + c - '0'))); // Bangla
            //string english_text = string.Concat(bengali_text.Select(c => (char)('0' + c - '\u09E6'))); //English
        }
    }
}
