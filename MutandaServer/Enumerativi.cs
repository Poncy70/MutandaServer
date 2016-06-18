using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderEntry.Net.Service
{
    //
    // Summary:
    //     Authentication providers supported by Mobile Services.
    public enum MobileServiceAuthenticationProvider
    {
        MicrosoftAccount = 0,
        Google = 1,
        Twitter = 2,
        Facebook = 3,
        WindowsAzureActiveDirectory = 4
    }

    public enum EntityState
    {
        Added = 0,
        Modify = 1,
        Cancel = 2
    }

    public enum DbParamType
    {
        None = 0,
        String = 1,
        Date = 2,
        Boolean = 3,
        Int = 4,
        Double = 6,
        Decimal = 7,
        Binary = 8
    }
}