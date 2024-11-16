SELECT id as "Id", email as "Email", hashed_password as "HashedPassword", name as "Name", surname as "Surname", patronymic as "Patronymic", role as "Role"
FROM USERS where @email = email