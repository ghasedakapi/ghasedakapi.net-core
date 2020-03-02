# Ghasedak.Core

  ghasedak.core .NET Core Helper Library 

## Adding Ghasedak.Core libraries to your .NET Core project

  The best and easiest way to add the Ghasedak.Core libraries to your .NET Core project is to use the NuGet package manager.

## Package Manager
   Install-Package Ghasedak.Core
## .NET CLI 
   dotnet add package Ghasedak.Core
   
## Simple Send

```c#

      try
            {
                var sms = new Ghasedak.Core.Api("apikey");
                var result = await sms.SendSMS("message", "0912xxxxxxx");
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

```

## Bulk Send
   
   ```c#
   
          try
            {
                var sms = new Ghasedak.Core.Api("apikey");
                var res = sms.SendSMS("message", "linenumber", new string[] { "0912xxxxxxx","0937xxxxxxxx" });
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
            
```

## Verify 

```c#

     try
            {
                var sms = new Ghasedak.Core.Api("apikey");
                var result = sms.Verify(1, "template", new string[] { "0912xxxxxxx", "0937xxxxxxxx" }, "test", "test2");
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

```
            
            

  
