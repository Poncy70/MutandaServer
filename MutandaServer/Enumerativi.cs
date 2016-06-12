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
}