CREATE FUNCTION insert_user(first_name VARCHAR, last_name VARCHAR) RETURNS void AS $$
    INSERT INTO users(first_name, last_name) VALUES (first_name, last_name);
$$ LANGUAGE SQL;