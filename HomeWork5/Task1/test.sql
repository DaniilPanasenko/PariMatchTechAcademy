SELECT insert_user('Daniil', 'Panasenko');
SELECT insert_user('Ivan', 'Ivanov');
SELECT insert_user('Petr', 'Petrov');

SELECT insert_note('7fd66fc4-5e66-11eb-ae93-0242ac130002', 'hw', 'hello world', 1);
SELECT insert_note('c0c7bad8-5e66-11eb-ae93-0242ac130002', 'test', 'test', 1);
SELECT insert_note('c7fdaa6a-5e66-11eb-ae93-0242ac130002', 'my_note', 'note', 2);
SELECT insert_note('cdfc38dc-5e66-11eb-ae93-0242ac130002', 'new_note', 'note', 3);
SELECT insert_note('d40db3d6-5e66-11eb-ae93-0242ac130002', 'note', 'note', 1);
SELECT insert_note('d9bdb4a2-5e66-11eb-ae93-0242ac130002', 'hi', 'hi world', 3);
SELECT insert_note('5504c25e-5e67-11eb-ae93-0242ac130002', 'pm', 'parimatch', 2);
SELECT insert_note('5d6f82b2-5e67-11eb-ae93-0242ac130002', 'parimatch', 'parimatch', 3);
SELECT insert_note('64f3eca8-5e67-11eb-ae93-0242ac130002', 'password', 'qwerty2020', 2);

SELECT select_note('5504c25e-5e67-11eb-ae93-0242ac130002');

SELECT select_note('64f3eca8-5e67-11eb-ae93-0242ac130002');

SELECT select_user_notes(1);

SELECT select_users_notes_count();

SELECT update_note_mark_deleted('d40db3d6-5e66-11eb-ae93-0242ac130002');

SELECT select_note('d40db3d6-5e66-11eb-ae93-0242ac130002');

SELECT select_user_notes(1);

SELECT update_note_mark_deleted('d9bdb4a2-5e66-11eb-ae93-0242ac130002');

SELECT select_users_notes_count();
