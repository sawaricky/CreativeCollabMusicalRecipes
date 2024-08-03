# Musical Recipes
Introduction
The Musical Recipes system showcases a Minimum Viable Product (MVP) with full Create, Read, Update, and Delete (CRUD) functionality. Based on feedback from team meetings, the system implements a fully functioning WebAPI and corresponding user interface to manage the data and relationships across all tables, from individual projects to the collaborated system.

Overview Collaborated Content:

Collaboration between 
Pavan Mistry (n01650001)
Akash Sharma (n01604846)


Project Components:

1. Music Management System
2. Recipe Management System

Aim: To create a Minimum Viable Product (MVP).

Project Integration:

1. Merged both projects to solve errors and debugging issues.
2. Combined and migrated code.
3. Updated database to reflect changes.

Features:

1. Integrated a dropdown in the lesson table to allow instructors to select the type of lesson (Music or Recipe) they will be teaching, combining both recipes and music lessons.
2. both projects Designed user interface using Bootstrap.
3. Implemented user authentication for adding, editing and deleting functionalities for all tables.
4. Provided comprehensive summary blocks for all code in the project.
5. responsive design

Areas of Problems Encountered

1. Solved a routing error for the Academy/AcademyData controllers when Creating an edit view
2. Encountered database errors when modifying tables to include new entity, solved them by setting nullable type value to recipeId.

CRUD Operations:

-Create: Add new entities to the database.
-Read: Retrieve and display entities from the database.
-Update: Modify existing entities in the database.
-Delete: Remove entities from the database.

Entity Relationships:

-Implement One-to-Many (1-M) where a musical recipe can have more than one lesson.
-Many lessons can be taught by one instructor.

WebAPI Controller:

-List Entities: Retrieve a list of all entities in the database (e.g., all lessons).
-Find Entity by ID: Retrieve a specific entity by its ID (e.g., a specific lesson).
-Create New Entity: Add a new entity to the database using POST input (e.g., adding more lessons).
-Update Entity: Modify an existing entity using POST input and an ID (e.g., updating a specific lesson).
-Delete Entity: Remove an entity from the database using its ID (e.g., deleting an existing lesson).
-List Associated Records: Retrieve associated records given an entity ID (e.g., listing the music or recipe for a lesson).
-Add New Association: Create a new association for a record (e.g., adding new recipes or music classes).
-Delete Association: Remove an association from a record (e.g., deleting a recipe or music class).

MVC Components:

-Controller: Handles user requests, processes data through the WebAPI, and returns appropriate views.
-Views: Render the following pages:
-Listing All Entities
-Showing an Entity
-Creating a New Entity
-Updating an Entity
-Deleting an Entity
-ViewModels: Serves between Views and data retrieved by the Controller.

 eedback:

-All feedback from the project plan has been fully implement. The CRUD functionality is completely implemented for both base entities and their relationships. The code followz professional standards and is ready for additional features.

Professional Standards:

-Code Quality: The codebase is clean, well-documented, and follows good practices for readability and maintainability.
-Testing: Testing has been performed to ensure functionality reliability and correctness.
-Security: Measures such as user authentication have been implemented to secure data and prevent unauthorized access for adding, deleting, and updating records.

Next Steps:

With core CRUD functionality and entity relationships in place, the project is now ready to develop additional features, including \ user interface components and integration with external services like printing.


Conclusion:
This MVP provides a solid foundation for managing entities and their relationships. The implementation meets all requirements based on the feedback provided during the proposal stage.

Contributors:

Akash Sharma:

-Created and pushed Instructor, Instrument, and Lesson Controllers, DataControllers, Views, Models, Migrations, and Classes.

Pavan Misrty:

-Created and pushed Ingredients, Recipe, and Instruction Controllers, DataControllers, Views, Models, Migrations, and Classes.

Combined Contributions:

-Proposal presentation
-Managing association files, Controllers, DataControllers, Views, Models, Migrations, and -Classes for Lesson and Recipe
-Designing a responsive user interface
-Managing GitHub to avoid conflicts
-Readme file
-Summary blocks
