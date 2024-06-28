
# DotNet OIDC Sandbox

This repo contains examples of securing different types of .NET and JavaScript web applications using OpenIdConnect.



## IdentityServer

These examples draw from the IdentityServer4 documentation (https://identityserver4.readthedocs.io) but aim to provide runnable, self-contained examples which can be stepped through in Visual Studio.


.NET starter templates for using IdentityServer4 can be installed with: 

```
dotnet new -i IdentityServer4.Templates
```


# Example Contents

## Identity Provider (IdP)
Each example contains a simple ODIC Identity Provider  project `IdP` which runs on `localhost:44398`. This is a simple ASP.NET Core web application which makes use of IdentityServer 4.

The Visual Studio solutions should have all projects set as startup projects.  The IdP must be running as well as the web applications using it.

Usernames and passwords can be found in the file `TestUsers.cs`.

## Greetings API
This is a simple Api with a single GET endpoint "`/greetings`" which returns a random greeting message on each call.

## Package Dependencies
The secured .NET Web Applications don't need to know anything about IdentityServer.  They make use of `Microsoft.AspNet.Authentication.JwtBearer` which will work in the same way regardless of the chosen IdP implementation.

.NET Client applications use the `IdentityModel` library and the Javascript examples uses `oidc-client` - both of which are from the developers of IdentityServer4.  These libraries make it easier to retrieve and process the tokens.  Again, these libraries are independent of the chosen IdP.

# Examples

## End To End 
(to be completed)

## Programatic Client

This solution contains a console application which uses Client Credentials flow to authenticate via the Idp and make a call to retrieve a greeting from the API.

## Secured SPA

This solution contains a simple JavaScript client which authenticates the user using Authorization Code flow with PKCE in order to retrieve a greeting from the API.

## Secured Web App
This solution contains a simple .NET client which authenticates the user using Authorization Code flow.  Retrieved user details are output.  

There is also an example of External Authentication using a Google Account.

## Secured Web App with API
This solution extends the Secured Web App example to call the greeting APi and add a message at the top of the screen, outputting the greeting before person's name.