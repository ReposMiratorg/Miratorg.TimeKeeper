add-migration InitializeDb
add-migration Update001
add-migration -Context TemplateDbContext Update001

update-database