CREATE FUNCTION insert_note(id UUID, header VARCHAR, body VARCHAR, user_id INT)
RETURNS void AS $$
    INSERT INTO notes(id, header, body, user_id, is_deleted) 
    VALUES (id, header, body, user_id, false);
$$ LANGUAGE SQL;