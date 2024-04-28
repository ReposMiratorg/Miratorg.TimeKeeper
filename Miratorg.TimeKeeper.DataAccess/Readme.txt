add-migration InitializeDb
add-migration Update001
add-migration -Context TimeKeeperDbContext Update001

update-database