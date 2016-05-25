BEGIN;

-- temperature id sequence
CREATE SEQUENCE IF NOT EXISTS public.temperature_id_seq
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;
	
-- temperature table
CREATE TABLE IF NOT EXISTS public.temperatures (
  id integer NOT NULL DEFAULT nextval('temperature_id_seq'::regclass),
  swimmer numeric(4,2),
  non_swimmer numeric(4,2),
  kids_splash numeric(4,2),
  measured_at timestamp without time zone,
  CONSTRAINT temperatures_pk PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);

-- rename primary key if it's wrong
ALTER INDEX IF EXISTS pk RENAME TO temperatures_pk;

-- delete duplicate measure data
DELETE FROM public.temperatures
WHERE id NOT IN (
	SELECT min(id)
	FROM public.temperatures
	GROUP BY measured_at
);

-- create air_temperatures sequence
CREATE SEQUENCE IF NOT EXISTS public.air_temperature_id_seq
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;

-- create air_tempeatures table
CREATE TABLE IF NOT EXISTS public.air_temperatures (
  id integer NOT NULL DEFAULT nextval('air_temperature_id_seq'::regclass),
  temperature numeric(4,2),
  relative_humidity numeric(4,2),
  station_id integer,
  quality_level_id integer,
  measured_at timestamp without time zone,
  CONSTRAINT air_temperatures_pk PRIMARY KEY(id)
)
WITH (
  OIDS=FALSE
);

-- create weather_stations sequence
CREATE SEQUENCE IF NOT EXISTS public.weather_station_id_seq
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;

-- create weather_stations table
CREATE TABLE IF NOT EXISTS public.weather_stations (
  id integer NOT NULL DEFAULT nextval('weather_station_id_seq'::regclass),
  station_id integer,
  name varchar(200),
  state varchar(100),
  geo_longitude numeric(6,4),
  geo_latitude numeric(6,4),
  CONSTRAINT weather_stations_pk PRIMARY KEY(id)
);

COMMIT;

-- add station_id to weather_stations via pgsql
do $$
begin
	IF NOT EXISTS (SELECT 0 FROM information_schema.columns WHERE table_schema = 'public' AND table_name = 'weather_stations' AND column_name = 'station_id')
	THEN
		ALTER TABLE public.weather_stations ADD COLUMN station_id integer;
	END IF;
end 
$$