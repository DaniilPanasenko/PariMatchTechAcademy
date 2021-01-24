CREATE FUNCTION select_user_notes(id_user INT)
RETURNS TABLE
        (
            id UUID,
            header VARCHAR,
            body VARCHAR,
            is_deleted BOOLEAN,
            user_id INT,
            modified_at TIMESTAMP WITH TIME ZONE
        ) AS $$
    SELECT * FROM notes WHERE user_id = id_user AND NOT is_deleted
$$ LANGUAGE SQL;
