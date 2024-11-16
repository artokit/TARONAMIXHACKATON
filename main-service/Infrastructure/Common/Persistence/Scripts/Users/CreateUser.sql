INSERT INTO USERS(email, hashed_password, name, surname, patronymic, role)
VALUES(@email, @hashed_password, @name, @surname, @patronymic, @role) 
RETURNING id as "Id", email as "Email", hashed_password as "HashedPassword", name as "Name", surname as "Surname", patronymic as "Patronymic", role as "Role"