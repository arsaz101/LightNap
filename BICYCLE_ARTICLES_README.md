# Bicycle Component Articles Management System

This document describes the bicycle component articles management system built for internal dashboard use. The system allows editorial teams to view, filter, and manage article listings for bicycle components submitted by suppliers.

## Features

### 🔹 Frontend (Angular 19+)

- **Article List View**: Display articles in a sortable, paginated table
- **Advanced Filtering**: Filter by article category, bicycle category (multi-select), material, and search terms
- **CRUD Operations**: Create, read, update, and delete articles
- **Modern UI**: Built with PrimeNG components and Tailwind CSS
- **Responsive Design**: Works on desktop and mobile devices

### 🔹 Backend (.NET 8+)

- **RESTful API**: Complete CRUD operations for articles
- **Advanced Filtering & Sorting**: Server-side filtering and sorting with pagination
- **Data Validation**: Input validation using Data Annotations
- **Database Support**: SQLite with Entity Framework Core
- **Authentication**: JWT-based authentication required for all operations

## Database Schema

### Article Entity

```csharp
public class Article
{
    public int Id { get; set; }
    public string ArticleNumber { get; set; }        // Unique article code
    public string Name { get; set; }                 // Article name
    public string ArticleCategory { get; set; }      // e.g., Hub, Crank, Wheel
    public string BicycleCategory { get; set; }      // e.g., Road, Mountain
    public string Material { get; set; }             // e.g., Aluminum, Carbon Fiber
    public decimal? LengthMm { get; set; }           // Length in millimeters
    public decimal? WidthMm { get; set; }            // Width in millimeters
    public decimal? HeightMm { get; set; }           // Height in millimeters
    public decimal? NetWeightG { get; set; }         // Net weight in grams
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
}
```

## API Endpoints

### Articles Controller (`/api/articles`)

| Method | Endpoint                           | Description                                       |
| ------ | ---------------------------------- | ------------------------------------------------- |
| GET    | `/api/articles`                    | Get paginated articles with filtering and sorting |
| GET    | `/api/articles/{id}`               | Get article by ID                                 |
| POST   | `/api/articles`                    | Create new article                                |
| PUT    | `/api/articles/{id}`               | Update existing article                           |
| DELETE | `/api/articles/{id}`               | Delete article                                    |
| GET    | `/api/articles/categories`         | Get all article categories                        |
| GET    | `/api/articles/bicycle-categories` | Get all bicycle categories                        |
| GET    | `/api/articles/materials`          | Get all materials                                 |

### Query Parameters for GET /api/articles

| Parameter         | Type   | Description                                               |
| ----------------- | ------ | --------------------------------------------------------- |
| `articleCategory` | string | Filter by article category                                |
| `bicycleCategory` | string | Filter by bicycle category (comma-separated for multiple) |
| `material`        | string | Filter by material                                        |
| `searchTerm`      | string | Search in article number and name                         |
| `sortBy`          | string | Sort field (articleNumber, name, articleCategory, etc.)   |
| `sortDirection`   | string | Sort direction (asc/desc)                                 |
| `page`            | int    | Page number (1-based)                                     |
| `pageSize`        | int    | Number of items per page                                  |

## Sample Data

The system comes pre-loaded with sample bicycle component articles:

### Hubs

- Shimano Deore XT Hub (Mountain, Aluminum)
- Campagnolo Record Hub (Road, Carbon Fiber)

### Cranksets

- SRAM GX Eagle Crankset (Mountain, Aluminum)
- Shimano Ultegra Crankset (Road, Aluminum)

### Wheels

- Mavic Cosmic Pro Carbon (Road, Carbon Fiber)
- DT Swiss XM 1700 (Mountain, Aluminum)

### Forks

- RockShox Pike Ultimate (Mountain, Aluminum)
- Fox 32 Step-Cast (Road, Carbon Fiber)

### Brakes

- Shimano XT M8100 (Mountain, Aluminum)
- SRAM Red eTap AXS (Road, Carbon Fiber)

## Getting Started

### Prerequisites

- .NET 8 SDK
- Node.js 18+ and npm
- SQLite (or SQL Server/PostgreSQL)

### Backend Setup

1. **Navigate to the backend directory:**

   ```bash
   cd src/LightNap.WebApi
   ```

2. **Update database connection** (if needed):

   - Edit `appsettings.json` to configure your database provider
   - Default is SQLite with in-memory database

3. **Run the application:**

   ```bash
   dotnet run
   ```

4. **Database migration** (if using persistent database):
   ```bash
   cd ../LightNap.DataProviders.Sqlite
   dotnet ef database update
   ```

### Frontend Setup

1. **Navigate to the Angular project:**

   ```bash
   cd src/lightnap-ng
   ```

2. **Install dependencies:**

   ```bash
   npm install
   ```

3. **Start the development server:**

   ```bash
   npm start
   ```

4. **Access the application:**
   - Open `http://localhost:4200`
   - Navigate to Admin → Articles

## Usage

### Viewing Articles

1. Navigate to the Articles page in the admin section
2. Use the filter controls to narrow down results:
   - **Search**: Type to search article numbers and names
   - **Article Category**: Select from dropdown (Hub, Crank, Wheel, etc.)
   - **Bicycle Category**: Multi-select from available categories
   - **Material**: Select from dropdown (Aluminum, Carbon Fiber, etc.)

### Sorting

- Click column headers to sort by that field
- Click again to reverse sort order
- Supported fields: Article Number, Name, Category, Material, Net Weight

### Adding Articles

1. Click the "Add Article" button
2. Fill in the required fields (marked with \*)
3. Optionally add dimensions and weight
4. Click "Save"

### Editing Articles

1. Click the edit (pencil) icon on any article row
2. Modify the fields as needed
3. Click "Save"

### Deleting Articles

1. Click the delete (trash) icon on any article row
2. Confirm the deletion in the dialog

## Architecture

### Backend Architecture

```
LightNap.Core/
├── Articles/
│   ├── Dto/
│   │   ├── Request/          # API request DTOs
│   │   └── Response/         # API response DTOs
│   ├── Interfaces/           # Service interfaces
│   └── Services/             # Business logic
├── Data/
│   └── Entities/             # Database entities
└── WebApi/
    └── Controllers/          # API controllers
```

### Frontend Architecture

```
src/app/admin/
├── components/pages/articles/
│   └── articles.component.ts # Main articles component
├── models/
│   └── article.model.ts      # TypeScript interfaces
└── services/
    └── articles.service.ts   # API service
```

## Customization

### Adding New Article Categories

1. Add the category to the sample data in `ArticleSeeder.cs`
2. The system will automatically populate the dropdown

### Adding New Bicycle Categories

1. Add the category to the sample data in `ArticleSeeder.cs`
2. The system will automatically populate the multi-select

### Adding New Materials

1. Add the material to the sample data in `ArticleSeeder.cs`
2. The system will automatically populate the dropdown

### Modifying the UI

- Edit `articles.component.ts` to modify the interface
- Update the template to change the layout
- Modify the service to add new API calls

## Security

- All API endpoints require authentication
- JWT tokens are used for authorization
- Input validation prevents malicious data
- SQL injection protection via Entity Framework

## Performance

- Server-side pagination for large datasets
- Database indexes on frequently queried fields
- Lazy loading in the Angular component
- Efficient filtering and sorting

## Troubleshooting

### Common Issues

1. **Database Connection Errors**

   - Check `appsettings.json` for correct connection string
   - Ensure database provider is properly configured

2. **API 401 Unauthorized**

   - Ensure you're logged in with admin privileges
   - Check JWT token is valid

3. **Angular Build Errors**

   - Run `npm install` to ensure all dependencies are installed
   - Check TypeScript compilation errors

4. **Migration Errors**
   - Delete existing database and recreate
   - Run `dotnet ef database update` again

### Logs

- Backend logs are available in the console output
- Check browser developer tools for frontend errors
- Database queries can be logged by enabling EF Core logging

## Future Enhancements

- **Bulk Operations**: Import/export articles via CSV/Excel
- **Image Support**: Add product images to articles
- **Supplier Management**: Link articles to suppliers
- **Advanced Analytics**: Usage statistics and reporting
- **API Rate Limiting**: Prevent abuse of the API
- **Audit Trail**: Track changes to articles over time

## Support

For issues or questions:

1. Check the troubleshooting section above
2. Review the API documentation
3. Check the application logs
4. Contact the development team
