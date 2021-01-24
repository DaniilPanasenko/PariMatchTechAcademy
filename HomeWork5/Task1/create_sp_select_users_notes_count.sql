CREATE FUNCTION select_users_notes_count()
RETURNS TABLE
        (
            id INT,
            first_name VARCHAR,
            last_name VARCHAR,
            count INT
        ) AS $$
    SELECT users.id, first_name, last_name, COUNT(notes)
    FROM users, notes
    WHERE users.id = notes.user_id AND NOT notes.is_deleted
    GROUP BY users.id
$$ LANGUAGE SQL;