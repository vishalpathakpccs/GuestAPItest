GuestAPI is an ASP.NET Core 3.1 Web API project that manages Guest entities. The API provides endpoints to add guests, retrieve guests, and add phone numbers to existing guests, among other features. It uses in-memory storage, follows Clean Architecture principles, and incorporates Serilog for logging.

### How to Run This Application

1. **Set the GuestAPI Project as the Startup Project:**
   - Right-click on the `GuestAPI` project in Solution Explorer.
   - Select **"Set as StartUp Project"**.

2. **Run the Application:**
   - Start the application by pressing **F5** or clicking the **"Start"** button in Visual Studio. This will run the application on HTTP.

3. **Open Swagger UI:**
   - Navigate to [http://localhost:5098/swagger/index.html](http://localhost:5098/swagger/index.html) in your web browser to access the Swagger UI.

4. **Authorize with API Key:**
   - In Swagger UI, use the **"Authorize"** button to pass the API Key: test.
   - Alternatively, in Postman, pass the API Key as a header: `ApiKey: test`.
   -ApiKey= "test"
5. **Test All the API Endpoints:**
   - Use the Swagger UI or Postman to test the various API endpoints provided by the application.

6. **Check Logs:**
   - View logs in the **"Output"** window in Visual Studio, which appears after running the application.

### Suggestions

1. **Use JWT Instead of API Key:**
   - Consider implementing JWT (JSON Web Token) authentication for enhanced security and better scalability.

2. **Implement Phone Number Validation:**
   - Ensure that phone number formats are validated properly to prevent incorrect or malformed data entries.

3. **Use a Database Instead of In-Memory Storage:**
   - Switch to a persistent database like SQL Server or any other relational database to maintain data across application restarts and provide more robust data management.