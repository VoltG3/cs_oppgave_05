CREATE TABLE IF NOT EXISTS actor (
    act_id INT PRIMARY KEY,
    act_fname VARCHAR(20) NOT NULL,
    act_lname VARCHAR(20) NOT NULL,
    act_gender CHAR(1) NOT NULL CHECK (act_gender IN ('M', 'F'))
);
