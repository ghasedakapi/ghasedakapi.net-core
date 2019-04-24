# Ghasedak.core

  ghasedak.core .NET Core Helper Library 

## Adding Ghasedak.core libraries to your .NET Core project

  The best and easiest way to add the Ghasedak.core libraries to your .NET Core project is to use the NuGet package manager.

## Package Manager
   Install-Package Ghasedak.Core -Version 1.0.0 
## .NET CLI 
   dotnet add package Ghasedak.Core --version 1.0.0
   
## Simple Send

```c#

      try
            {
                var sms = new Ghasedak.Core.Api("e50077bcc301871bc989b03079d55e15027b0ae7cabe555773874e8b115b27bb");
                var result = await sms.SendSMS("تست", "09378108880");
                foreach (var item in result.Items)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
           }





          try 
            {
                var sms = new Ghasedak.Core.Api("apikey");
                var result = sms.SendSMS("message", "receptor", "lineNumber");
                foreach ( var item in result.Items)
                 {
                    Console.WriteLine (item);
                 }
            }
            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }  
```

## Bulk Send
   
   ```c#
   
          try
            {
                var bulksms = new Ghasedak.Core.Api("apikey");
                var res = bulksms.SendSMS("message", "linenumber", new string[] { "0912xxxxxxx","0937xxxxxxxx" });
                foreach(var item in res.Items)
                {
                    Console.WriteLine("messageids:" + item);
                }
            }
            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            

  
