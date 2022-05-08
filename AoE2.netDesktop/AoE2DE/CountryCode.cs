﻿namespace AoE2NetDesktop.AoE2DE;

using System.Collections.Generic;

/// <summary>
/// Country code(ISO 3166-1).
/// </summary>
public static class CountryCode
{
    /// <summary>
    /// List of ISO-3166-1 alpha-2.
    /// </summary>
    public static readonly Dictionary<string, string> ISO31661alpha2 = new () {
        { "AD", "Andorra" },
        { "AE", "United Arab Emirates" },
        { "AF", "Afghanistan" },
        { "AG", "Antigua and Barbuda" },
        { "AI", "Anguilla" },
        { "AL", "Albania" },
        { "AM", "Armenia" },
        { "AN", "Netherlands Antilles" },
        { "AO", "Angola" },
        { "AQ", "Antarctica" },
        { "AR", "Argentina" },
        { "AS", "American Samoa" },
        { "AT", "Austria" },
        { "AU", "Australia" },
        { "AW", "Aruba" },
        { "AZ", "Azerbaijan" },
        { "BA", "Bosnia and Herzegovina" },
        { "BB", "Barbados" },
        { "BD", "Bangladesh" },
        { "BE", "Belgium" },
        { "BF", "Burkina Faso" },
        { "BG", "Bulgaria" },
        { "BH", "Bahrain" },
        { "BI", "Burundi" },
        { "BJ", "Benin" },
        { "BL", "Saint Barthelemy" },
        { "BM", "Bermuda" },
        { "BN", "Brunei" },
        { "BO", "Bolivia" },
        { "BR", "Brazil" },
        { "BS", "Bahamas" },
        { "BT", "Bhutan" },
        { "BW", "Botswana" },
        { "BY", "Belarus" },
        { "BZ", "Belize" },
        { "CA", "Canada" },
        { "CC", "Cocos Islands" },
        { "CD", "Democratic Republic of the Congo" },
        { "CF", "Central African Republic" },
        { "CG", "Republic of the Congo" },
        { "CH", "Switzerland" },
        { "CI", "Ivory Coast" },
        { "CK", "Cook Islands" },
        { "CL", "Chile" },
        { "CM", "Cameroon" },
        { "CN", "China" },
        { "CO", "Colombia" },
        { "CR", "Costa Rica" },
        { "CU", "Cuba" },
        { "CV", "Cape Verde" },
        { "CW", "Curacao" },
        { "CX", "Christmas Island" },
        { "CY", "Cyprus" },
        { "CZ", "Czech Republic" },
        { "DE", "Germany" },
        { "DJ", "Djibouti" },
        { "DK", "Denmark" },
        { "DM", "Dominica" },
        { "DO", "Dominican Republic" },
        { "DZ", "Algeria" },
        { "EC", "Ecuador" },
        { "EE", "Estonia" },
        { "EG", "Egypt" },
        { "EH", "Western Sahara" },
        { "ER", "Eritrea" },
        { "ES", "Spain" },
        { "ET", "Ethiopia" },
        { "FI", "Finland" },
        { "FJ", "Fiji" },
        { "FK", "Falkland Islands" },
        { "FM", "Micronesia" },
        { "FO", "Faroe Islands" },
        { "FR", "France" },
        { "GA", "Gabon" },
        { "GB", "United Kingdom" },
        { "GD", "Grenada" },
        { "GE", "Georgia" },
        { "GG", "Guernsey" },
        { "GH", "Ghana" },
        { "GI", "Gibraltar" },
        { "GL", "Greenland" },
        { "GM", "Gambia" },
        { "GN", "Guinea" },
        { "GQ", "Equatorial Guinea" },
        { "GR", "Greece" },
        { "GT", "Guatemala" },
        { "GU", "Guam" },
        { "GW", "Guinea-Bissau" },
        { "GY", "Guyana" },
        { "HK", "Hong Kong" },
        { "HN", "Honduras" },
        { "HR", "Croatia" },
        { "HT", "Haiti" },
        { "HU", "Hungary" },
        { "ID", "Indonesia" },
        { "IE", "Ireland" },
        { "IL", "Israel" },
        { "IM", "Isle of Man" },
        { "IN", "India" },
        { "IO", "British Indian Ocean Territory" },
        { "IQ", "Iraq" },
        { "IR", "Iran" },
        { "IS", "Iceland" },
        { "IT", "Italy" },
        { "JE", "Jersey" },
        { "JM", "Jamaica" },
        { "JO", "Jordan" },
        { "JP", "Japan" },
        { "KE", "Kenya" },
        { "KG", "Kyrgyzstan" },
        { "KH", "Cambodia" },
        { "KI", "Kiribati" },
        { "KM", "Comoros" },
        { "KN", "Saint Kitts and Nevis" },
        { "KP", "North Korea" },
        { "KR", "South Korea" },
        { "KW", "Kuwait" },
        { "KY", "Cayman Islands" },
        { "KZ", "Kazakhstan" },
        { "LA", "Laos" },
        { "LB", "Lebanon" },
        { "LC", "Saint Lucia" },
        { "LI", "Liechtenstein" },
        { "LK", "Sri Lanka" },
        { "LR", "Liberia" },
        { "LS", "Lesotho" },
        { "LT", "Lithuania" },
        { "LU", "Luxembourg" },
        { "LV", "Latvia" },
        { "LY", "Libya" },
        { "MA", "Morocco" },
        { "MC", "Monaco" },
        { "MD", "Moldova" },
        { "ME", "Montenegro" },
        { "MF", "Saint Martin" },
        { "MG", "Madagascar" },
        { "MH", "Marshall Islands" },
        { "MK", "Macedonia" },
        { "ML", "Mali" },
        { "MM", "Myanmar" },
        { "MN", "Mongolia" },
        { "MO", "Macau" },
        { "MP", "Northern Mariana Islands" },
        { "MR", "Mauritania" },
        { "MS", "Montserrat" },
        { "MT", "Malta" },
        { "MU", "Mauritius" },
        { "MV", "Maldives" },
        { "MW", "Malawi" },
        { "MX", "Mexico" },
        { "MY", "Malaysia" },
        { "MZ", "Mozambique" },
        { "NA", "Namibia" },
        { "NC", "New Caledonia" },
        { "NE", "Niger" },
        { "NG", "Nigeria" },
        { "NI", "Nicaragua" },
        { "NL", "Netherlands" },
        { "NO", "Norway" },
        { "NP", "Nepal" },
        { "NR", "Nauru" },
        { "NU", "Niue" },
        { "NZ", "New Zealand" },
        { "OM", "Oman" },
        { "PA", "Panama" },
        { "PE", "Peru" },
        { "PF", "French Polynesia" },
        { "PG", "Papua New Guinea" },
        { "PH", "Philippines" },
        { "PK", "Pakistan" },
        { "PL", "Poland" },
        { "PM", "Saint Pierre and Miquelon" },
        { "PN", "Pitcairn" },
        { "PR", "Puerto Rico" },
        { "PS", "Palestine" },
        { "PT", "Portugal" },
        { "PW", "Palau" },
        { "PY", "Paraguay" },
        { "QA", "Qatar" },
        { "RE", "Reunion" },
        { "RO", "Romania" },
        { "RS", "Serbia" },
        { "RU", "Russia" },
        { "RW", "Rwanda" },
        { "SA", "Saudi Arabia" },
        { "SB", "Solomon Islands" },
        { "SC", "Seychelles" },
        { "SD", "Sudan" },
        { "SE", "Sweden" },
        { "SG", "Singapore" },
        { "SH", "Saint Helena" },
        { "SI", "Slovenia" },
        { "SJ", "Svalbard and Jan Mayen" },
        { "SK", "Slovakia" },
        { "SL", "Sierra Leone" },
        { "SM", "San Marino" },
        { "SN", "Senegal" },
        { "SO", "Somalia" },
        { "SR", "Suriname" },
        { "SS", "South Sudan" },
        { "ST", "Sao Tome and Principe" },
        { "SV", "El Salvador" },
        { "SX", "Sint Maarten" },
        { "SY", "Syria" },
        { "SZ", "Swaziland" },
        { "TC", "Turks and Caicos Islands" },
        { "TD", "Chad" },
        { "TG", "Togo" },
        { "TH", "Thailand" },
        { "TJ", "Tajikistan" },
        { "TK", "Tokelau" },
        { "TL", "East Timor" },
        { "TM", "Turkmenistan" },
        { "TN", "Tunisia" },
        { "TO", "Tonga" },
        { "TR", "Turkey" },
        { "TT", "Trinidad and Tobago" },
        { "TV", "Tuvalu" },
        { "TW", "Taiwan" },
        { "TZ", "Tanzania" },
        { "UA", "Ukraine" },
        { "UG", "Uganda" },
        { "US", "United States" },
        { "UY", "Uruguay" },
        { "UZ", "Uzbekistan" },
        { "VA", "Vatican" },
        { "VC", "Saint Vincent and the Grenadines" },
        { "VE", "Venezuela" },
        { "VG", "British Virgin Islands" },
        { "VI", "U.S. Virgin Islands" },
        { "VN", "Vietnam" },
        { "VU", "Vanuatu" },
        { "WF", "Wallis and Futuna" },
        { "WS", "Samoa" },
        { "XK", "Kosovo" },
        { "YE", "Yemen" },
        { "YT", "Mayotte" },
        { "ZA", "South Africa" },
        { "ZM", "Zambia" },
        { "ZW", "Zimbabwe" },
    };

    /// <summary>
    /// convert country code to country name.
    /// </summary>
    /// <param name="countryCode">country code.</param>
    /// <returns>country name.</returns>
    public static string ConvertToFullName(string countryCode)
    {
        string countryName;
        if (countryCode == null) {
            countryName = "N/A";
        } else {
            if (!ISO31661alpha2.TryGetValue(countryCode, out countryName)) {
                countryName = "N/A";
            }
        }

        return countryName;
    }
}
