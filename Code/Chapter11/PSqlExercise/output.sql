CREATE TABLE "Books" (
    "BookId" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Url" text NULL,
    "_privateKey" text NULL,
    CONSTRAINT "PK_Books" PRIMARY KEY ("BookId")
);


CREATE TABLE "Cars" (
    "LicensePlate" text NOT NULL,
    "State" text NOT NULL,
    "DisplayName" text GENERATED ALWAYS AS ("State" || ', ' ||  "LicensePlate") STORED,
    "Make" character varying(50) NOT NULL,
    "Model" character varying(10) NOT NULL,
    CONSTRAINT "PK_Cars" PRIMARY KEY ("State", "LicensePlate")
);


CREATE TABLE "NameMaps" (
    "NameMapId" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    CONSTRAINT "PK_NameMaps" PRIMARY KEY ("NameMapId")
);
COMMENT ON TABLE "NameMaps" IS 'NameMap by Edward';


CREATE TABLE "Tags" (
    "TagId" text NOT NULL,
    CONSTRAINT "PK_Tags" PRIMARY KEY ("TagId")
);


CREATE TABLE "Blogs" (
    "BlogId" integer NOT NULL,
    "Url" text NULL,
    "NameMapId" integer NOT NULL,
    CONSTRAINT "PK_Blogs" PRIMARY KEY ("BlogId"),
    CONSTRAINT "FK_Blogs_NameMaps_NameMapId" FOREIGN KEY ("NameMapId") REFERENCES "NameMaps" ("NameMapId") ON DELETE RESTRICT
);
COMMENT ON TABLE "Blogs" IS 'Blogs by Edward';


CREATE TABLE "Posts" (
    "PostId" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Title" varchar(200) NULL,
    "Content" character varying(500) NOT NULL,
    "Pay" numeric(14,2) NOT NULL,
    "LastUpated" timestamp without time zone NOT NULL,
    "MainBlogId" integer NOT NULL,
    "SubBlogId" integer NOT NULL,
    "NameMapId" integer NOT NULL,
    CONSTRAINT "PK_Posts" PRIMARY KEY ("PostId"),
    CONSTRAINT "FK_Posts_Blogs_MainBlogId" FOREIGN KEY ("MainBlogId") REFERENCES "Blogs" ("BlogId") ON DELETE CASCADE,
    CONSTRAINT "FK_Posts_Blogs_SubBlogId" FOREIGN KEY ("SubBlogId") REFERENCES "Blogs" ("BlogId") ON DELETE CASCADE,
    CONSTRAINT "FK_Posts_NameMaps_NameMapId" FOREIGN KEY ("NameMapId") REFERENCES "NameMaps" ("NameMapId") ON DELETE CASCADE
);
COMMENT ON TABLE "Posts" IS 'Posts by Edward';
COMMENT ON COLUMN "Posts"."Title" IS 'The Title of the Post';


CREATE TABLE "PostTags" (
    "PostId" integer NOT NULL,
    "TagId" text NOT NULL,
    "PublicationDate" timestamp without time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT "PK_PostTags" PRIMARY KEY ("PostId", "TagId"),
    CONSTRAINT "FK_PostTags_Posts_PostId" FOREIGN KEY ("PostId") REFERENCES "Posts" ("PostId") ON DELETE CASCADE,
    CONSTRAINT "FK_PostTags_Tags_TagId" FOREIGN KEY ("TagId") REFERENCES "Tags" ("TagId") ON DELETE CASCADE
);


INSERT INTO "NameMaps" ("NameMapId", "Name")
VALUES (1, '中視');
INSERT INTO "NameMaps" ("NameMapId", "Name")
VALUES (2, '台視');
INSERT INTO "NameMaps" ("NameMapId", "Name")
VALUES (3, '華視');


INSERT INTO "Blogs" ("BlogId", "NameMapId", "Url")
VALUES (1, 1, 'http://1.com');
INSERT INTO "Blogs" ("BlogId", "NameMapId", "Url")
VALUES (2, 2, 'http://2.com');
INSERT INTO "Blogs" ("BlogId", "NameMapId", "Url")
VALUES (3, 3, 'http://3.com');


INSERT INTO "Posts" ("PostId", "Content", "LastUpated", "MainBlogId", "NameMapId", "Pay", "SubBlogId", "Title")
VALUES (1, '第一篇內容', TIMESTAMP '2021-03-28 16:30:21.293915', 1, 1, 0.0, 2, '第一篇');
INSERT INTO "Posts" ("PostId", "Content", "LastUpated", "MainBlogId", "NameMapId", "Pay", "SubBlogId", "Title")
VALUES (2, '第二篇內容', TIMESTAMP '2021-03-28 16:30:21.293976', 1, 2, 0.0, 3, '第二篇');


CREATE UNIQUE INDEX "IX_Blogs_NameMapId" ON "Blogs" ("NameMapId");


CREATE INDEX "IX_Posts_MainBlogId" ON "Posts" ("MainBlogId");


CREATE UNIQUE INDEX "IX_Posts_NameMapId" ON "Posts" ("NameMapId");


CREATE INDEX "IX_Posts_SubBlogId" ON "Posts" ("SubBlogId");


CREATE INDEX "IX_PostTags_TagId" ON "PostTags" ("TagId");


SELECT setval(
    pg_get_serial_sequence('"NameMaps"', 'NameMapId'),
    GREATEST(
        (SELECT MAX("NameMapId") FROM "NameMaps") + 1,
        nextval(pg_get_serial_sequence('"NameMaps"', 'NameMapId'))),
    false);
SELECT setval(
    pg_get_serial_sequence('"Posts"', 'PostId'),
    GREATEST(
        (SELECT MAX("PostId") FROM "Posts") + 1,
        nextval(pg_get_serial_sequence('"Posts"', 'PostId'))),
    false);



