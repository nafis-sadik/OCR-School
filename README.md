# OCR-School

//the below code was excecuted to allow insertion without primary key values from foreign key table
//after excecuting this in database some other issues arised.
show variables like "sql_mode";
SET GLOBAL sql_mode = '';
