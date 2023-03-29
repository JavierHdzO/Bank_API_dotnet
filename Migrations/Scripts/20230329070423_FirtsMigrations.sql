CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Roles" (
    "RoleId" bigint GENERATED BY DEFAULT AS IDENTITY,
    "Type" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (now()),
    CONSTRAINT "PK_Roles" PRIMARY KEY ("RoleId")
);

CREATE TABLE "Users" (
    "UserId" bigint GENERATED BY DEFAULT AS IDENTITY,
    "Email" text NOT NULL,
    "Password" text NOT NULL,
    "Status" boolean NOT NULL DEFAULT TRUE,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (now()),
    "RoleId" bigint NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("UserId"),
    CONSTRAINT "FK_Users_Roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("RoleId") ON DELETE CASCADE
);

CREATE TABLE "Client" (
    "ClientId" bigint GENERATED BY DEFAULT AS IDENTITY,
    "Name" varchar(50) NOT NULL,
    "LastName" varchar(50) NOT NULL,
    "Age" smallint NOT NULL,
    "Genre" varchar(1) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (now()),
    "UserId" bigint NOT NULL,
    CONSTRAINT "PK_Client" PRIMARY KEY ("ClientId"),
    CONSTRAINT "FK_Client_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId") ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_Client_UserId" ON "Client" ("UserId");

CREATE INDEX "IX_Users_RoleId" ON "Users" ("RoleId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230329070423_FirtsMigrations', '7.0.4');

COMMIT;