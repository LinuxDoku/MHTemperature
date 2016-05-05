-- temperature id sequence
CREATE SEQUENCE public.temperature_id_seq
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 7
  CACHE 1;

-- temperature table
CREATE TABLE public.temperatures
(
  id integer NOT NULL DEFAULT nextval('temperature_id_seq'::regclass),
  swimmer numeric(4,2),
  non_swimmer numeric(4,2),
  kids_splash numeric(4,2),
  measured_at timestamp without time zone,
  CONSTRAINT pk PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);