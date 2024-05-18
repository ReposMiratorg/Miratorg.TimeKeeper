add-migration InitializeDb
add-migration Update001
add-migration -Context TimeKeeperDbContext Update020

update-database