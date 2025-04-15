## Run SQL Script

```bash
sqlcmd -S localhost -d FoodtekDB -E -i "files_path/file_name.sql"
```

to run all insert scripts in a folder:

```bash
sqlcmd -S localhost -d FoodtekDB -E -i "files_path/*.sql"
```
