add-migration InitializeDb
add-migration Update001
add-migration -Context TimeKeeperDbContext Update010

update-database