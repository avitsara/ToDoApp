CREATE TABLE User (
    user_id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY, 
    username VARCHAR(50) NOT NULL,                      
    email VARCHAR(100) NOT NULL UNIQUE,                  
    password_hash VARCHAR(255) NOT NULL,                 
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP      
);