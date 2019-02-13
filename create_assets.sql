-- Drop table

-- DROP TABLE public.assets

CREATE TABLE public.assets (
	"Name" varchar(32767) NULL,
	hp varchar(32767) NULL,
	"Attack" varchar(32767) NULL,
	"Counterattack" varchar(32767) NULL,
	"Id" int4 NOT NULL,
	"AttackStats" varchar(32767) NULL,
	"AttackDice" varchar(32767) NULL,
	"DefenderReroll" varchar(32767) NULL,
	"AttackerReroll" varchar(32767) NULL,
	"AttackerExtraDice" varchar(32767) NULL,
	"DefenderExtraDice" varchar(32767) NULL,
	"Description" varchar(32767) NULL,
	"Type" varchar(32767) NULL
);
