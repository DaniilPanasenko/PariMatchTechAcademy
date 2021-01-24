CREATE TABLE notes(
    id UUID PRIMARY KEY,
    header VARCHAR(128) NOT NULL,
    body VARCHAR(128) NOT NULL,
    is_deleted BOOLEAN NOT NULL,
    user_id INT NOT NULL ,
    modified_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT current_timestamp,
    CONSTRAINT fk_user_id FOREIGN KEY (user_id) REFERENCES users(id)
);
CREATE INDEX ON notes(modified_at);