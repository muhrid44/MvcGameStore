# MvcGameStore

How to setup :

1. Clone this repository to your local repository
2. Replace the connection string with your database / your local database
   - Open file 'appsettings.Development.json'
   - Replace the value of "MVcGameStoreContext" with your database
3. Open Package Manager Console and set Default project to 'MvcGameStore'
4. type 'Update-Database' then press enter for starting migration data
5. Make sure the startup project is 'MvcGameStore'
6. Launch application with / without debugging
