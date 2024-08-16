# Musical Recipes

## Introduction

The Musical Recipes system showcases a Minimum Viable Product (MVP) with full Create, Read, Update, and Delete (CRUD) functionality. Based on feedback from team meetings, the system implements a fully functioning WebAPI and corresponding user interface to manage the data and relationships across all tables, from individual projects to the collaborated system.

### Features
- **Manage Recipes:** Create, Read, Update, and Delete recipe data.
- **Ingredient Management:** Manage ingredients linked to recipes.
- **Instruction Management:** Manage instructions linked to recipes.
- **Lesson Management:** Manage lessons linked to recipes and instructor.
- **Instructor Management:** Manage lessons linked to academy.
- **Relational Data:** Handle relationships between recipes, ingredients, instructions, lessons , instructors and academy.

### Overview Collaborated Content:

Collaboration between:
- Pavan Mistry (n01650001)
- Akash Sharma (n01604846)

## Project Components:

1. Music Management System
2. Recipe Management System

### Project Integration:

1. Merged both projects to solve errors and debugging issues.
2. Combined and migrated code.
3. Updated database to reflect changes.

### Features:

1. Integrated a dropdown in the lesson table to allow instructors to select the type of lesson (Music or Recipe) they will be teaching, combining both recipes and music lessons.
2. Designed user interface using Bootstrap.
3. Implemented user authentication for adding, editing, and deleting functionalities for all tables.
4. Provided comprehensive summary blocks for all code in the project.
5. Responsive design.

## Areas of Problems Encountered

1. Solved a routing error for the Academy/AcademyData controllers when creating an edit view.
2. Encountered database errors when modifying tables to include new entities, solved by setting nullable type values to `recipeId`.

## Running this Project
### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2

### Setup Steps
1. **Clone the Repository:**
   ```bash
   git clone https://github.com/sawaricky/CreativeCollabMusicalRecipes.git
   ```
   
2. **Open the Project:**
   Open the solution file `RecipeManagementApp.sln` in Visual Studio.

3. **Set Target Framework:**
   - Project > RecipeManagementApp Properties > Change target framework to 4.7.1 -> Change back to 4.7.2

4. **Create Database:**
   - Make sure there is an `App_Data` folder in the project (Right-click solution > View in File Explorer, then create the folder if it doesn't exist).
   - Tools > NuGet Package Manager > Package Manager Console > Run command: `Update-Database`
   - Ensure the database is created using:
     - View > SQL Server Object Explorer > MSSQLLocalDb > ..

5. **Run the Application:** 
   You can run the application using Visual Studio by hitting `F5` or selecting Debug > Start Debugging.
   
### Database Relationships
- **Recipe:** A recipe has many ingredients, many instructions and many lessons.
- **Ingredient:** Each ingredient belongs to a recipe.
- **Instruction:** Each instruction belongs to a recipe.
- **Lesson:** Each lesson belongs to a recipe and a instructor.
- **Instructor:** Many lessons can be taught by one instructor.

### FoodAdmin vs MusicAdmin
- Register an account
- View > SQL Server Object Explorer
- Create 'FoodAdmin', 'MusicAdmin' entries in AspNetRoles
- Copy UserID from AspNetUsers table
- Create entry between FoodAdmin Role x User Id, MusicAdmin Role x User Id in AspNetUserRoles bridging table

### WebAPI Controller:

- **List Entities:** Retrieve a list of all entities in the database (e.g., all lessons).
- **Find Entity by ID:** Retrieve a specific entity by its ID (e.g., a specific lesson).
- **Create New Entity:** Add a new entity to the database using POST input (e.g., adding more lessons).
- **Update Entity:** Modify an existing entity using POST input and an ID (e.g., updating a specific lesson).
- **Delete Entity:** Remove an entity from the database using its ID (e.g., deleting an existing lesson).
- **List Associated Records:** Retrieve associated records given an entity ID (e.g., listing the music or recipe for a lesson).
- **Add New Association:** Create a new association for a record (e.g., adding new recipes or music classes).
- **Delete Association:** Remove an association from a record (e.g., deleting a recipe or music class).

### MVC Components:

- **Controller:** Handles user requests, processes data through the WebAPI, and returns appropriate views.
- **Views:** Render the following pages:
  - Listing All Entities
  - Showing an Entity
  - Creating a New Entity
  - Updating an Entity
  - Deleting an Entity
- **ViewModels:** Serves as an intermediary between Views and data retrieved by the Controller.

## Feedback:

All feedback from the project plan has been fully implemented. The CRUD functionality is completely implemented for both base entities and their relationships. The code follows professional standards and is ready for additional features.

### Professional Standards:

- **Code Quality:** The codebase is clean, well-documented, and follows good practices for readability and maintainability.
- **Testing:** Testing has been performed to ensure functionality reliability and correctness.
- **Security:** Measures such as user authentication have been implemented to secure data and prevent unauthorized access for adding, deleting, and updating records.

## Next Steps:

With core CRUD functionality and entity relationships in place, the project is now ready to develop additional features, including user interface components and integration with external services like printing.

## Conclusion

This MVP provides a solid foundation for managing entities and their relationships. The implementation meets all requirements based on the feedback provided during the proposal stage.

## Contributors:

**Akash Sharma:**
- Created and pushed Instructor, Instrument, and Lesson Controllers, DataControllers, Views, Models, Migrations, and Classes.

**Pavan Mistry:**
- Created and pushed Ingredients, Recipe, and Instruction Controllers, DataControllers, Views, Models, Migrations, and Classes. Added Authentication with roles in Instructor, Instrument, and Lesson Controllers and DataControllers. Added extra features including a total count of Recipes, Lessons and Instructors in the home page of the project and to have a print recipe option that is handly for instructors to serve as notes during lesson.

**Combined Contributions:**
- Proposal presentation.
- Managing association files, Controllers, DataControllers, Views, Models, Migrations, and Classes for Lesson and Recipe.
- Designing a responsive user interface.
- Managing GitHub to avoid conflicts.
- Readme file.
- Summary blocks.
