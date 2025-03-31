# Database Guide

## Shared Attributes

- **Id**: Unique identifier for each record.
- **CreatedBy**: Identifier of the user who created the record.
- **CreatedAt**: Timestamp of when the record was created. default: current timestamp
- **UpdatedBy**: Identifier of the user who last updated the record.
- **UpdatedAt**: Timestamp of when the record was last updated. default: current timestamp
- **IsActive**: Boolean flag indicating if the record is active or inactive. default: true

```sql
CREATE TABLE shared_attributes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    ...
);
```

## Validation Rules

| Rule                                      | Constraint                                   |
| ----------------------------------------- | -------------------------------------------- |
| Requiring at Least One Number             | `value LIKE '%[0-9]%'`                       |
| Disallowing Numbers                       | `value NOT LIKE '%[0-9]%'`                   |
| Requiring at Least One Letter             | `value LIKE '%[a-zA-Z]%'`                    |
| Requiring at Least One Special Character  | `value LIKE '%[^a-zA-Z0-9]%'`                |
| Disallowing Special Characters            | `value NOT LIKE '%[^a-zA-Z0-9 ]%'`           |
| Allowing Exactly One Whitespace Character | `value LIKE '% %' AND value NOT LIKE '%  %'` |

### Allowing Only Numbers

- `value NOT LIKE '%[^0-9]%'`

### Allowing Only English Letters

- `value NOT LIKE '%[^a-zA-Z]%'`

### Allowing Only Arabic Letters

- `value NOT LIKE '%[^\u0600-\u06FF]%'`

### Positive Float Value Grate Than Zero

- `value > 0`

## Allow Whitespace

- `value LIKE '% %'`
