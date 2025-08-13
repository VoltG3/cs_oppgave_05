CREATE TABLE IF NOT EXISTS actor (
    act_id INT NOT NULL AUTO_INCREMENT,
    act_fname VARCHAR(20) NOT NULL,
    act_lname VARCHAR(20) NOT NULL,
    act_gender CHAR(1) NOT NULL CHECK (act_gender IN ('M', 'F')),
    PRIMARY KEY (act_id)                            
);
