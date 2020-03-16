solvled the assignment1.  

Assignment 2a for the music store 


1. Ensure your connectionstring in appsettings.json is called “DefaultConnection”   "done"

2. If you removed the Register and Login links to your navbar in Assignment 1, add these links
back to the navbar (if you already have them then you don’t need to do anything here)       "done"

3. In your assignment database, run the Identity.sql script attached to the assignment on
Blackboard. This will create the necessary ASPNET Identity tables in your database.    "done"

4. Create the other necessary ASP.Identity structure in your project:
a. Configure Identity in Startup.cs
b. Change your DbContext class so it inherits from IdentityDbContext              "done"

5. Once Authentication is working, add code to your navbar so that:
a. Register and Login show when the user is anonymous
b. Register and Login are replaced in the header by the username and Logout when the
user is authenticated. You can use the code from our in-class application to enable this.
c. Please create an account for me with these credentials:
i. marie@gc.ca / Test123$                                                        "done"

6. Modify the site in the following ways:
a. Make all Views where users can add, edit, or delete data PRIVATE, so only
authenticated users can access them
b. On your Index views, anonymous users can view the list of data but cannot see the
Create, Edit, or Delete links                                                    "done "

7. Enable Social Authentication with Google. Create new keys for your assignment rather than
using your existing Ctrl-Alt-PC keys. Store all API Keys in your appsettings.json file rather
than inside your C# code. ***If you are able to publish your site to Azure, you MUST also
add the Azure redirect url to the Google Console or your Google login will only work
when running locally.***                                                          "done"     


azure : ---- >>>  https://assignmentmusic20200223035726.azurewebsites.net 

                            