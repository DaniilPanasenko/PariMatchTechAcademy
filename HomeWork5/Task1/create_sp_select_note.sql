CREATE FUNCTION select_note(note_id UUID) RETURNS TABLE
    (
        id UUID,
        header VARCHAR,
        body VARCHAR,
        is_deleted BOOLEAN,
        modified_at TIMESTAMP WITH TIME ZONE,
        user_id INT,
        first_name VARCHAR,
        last_name VARCHAR
    ) AS $$
    SELECT notes.id,
           notes.header,
           notes.body,
           notes.is_deleted,
           notes.modified_at,
           users.id,
           users.first_name,
           users.last_name
    FROM notes, users
    WHERE notes.user_id = users.id AND note_id = notes.id
 $$ LANGUAGE SQL;