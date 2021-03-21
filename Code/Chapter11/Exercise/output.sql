CREATE TABLE "NameMaps" (
    "NameMapId" INTEGER NOT NULL CONSTRAINT "PK_NameMaps" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL
);


CREATE TABLE "Blogs" (
    -- Blogs by Edward

    "BlogId" INTEGER NOT NULL CONSTRAINT "PK_Blogs" PRIMARY KEY AUTOINCREMENT,
    "Url" TEXT NULL,
    "NameMapId" INTEGER NOT NULL,
    CONSTRAINT "FK_Blogs_NameMaps_NameMapId" FOREIGN KEY ("NameMapId") REFERENCES "NameMaps" ("NameMapId") ON DELETE RESTRICT
);


CREATE TABLE "Posts" (
    -- Posts by Edward

    "PostId" INTEGER NOT NULL CONSTRAINT "PK_Posts" PRIMARY KEY AUTOINCREMENT,

    -- The Title of the Post
    "Title" varchar(200) NULL,

    "Content" TEXT NOT NULL,

    "Pay" TEXT NOT NULL,

    "LastUpated" TEXT NOT NULL,

    "MainBlogId" INTEGER NOT NULL,

    "SubBlogId" INTEGER NOT NULL,

    "NameMapId" INTEGER NOT NULL,
    CONSTRAINT "FK_Posts_Blogs_MainBlogId" FOREIGN KEY ("MainBlogId") REFERENCES "Blogs" ("BlogId") ON DELETE CASCADE,
    CONSTRAINT "FK_Posts_Blogs_SubBlogId" FOREIGN KEY ("SubBlogId") REFERENCES "Blogs" ("BlogId") ON DELETE CASCADE,
    CONSTRAINT "FK_Posts_NameMaps_NameMapId" FOREIGN KEY ("NameMapId") REFERENCES "NameMaps" ("NameMapId") ON DELETE CASCADE
);


CREATE UNIQUE INDEX "IX_Blogs_NameMapId" ON "Blogs" ("NameMapId");


CREATE INDEX "IX_Posts_MainBlogId" ON "Posts" ("MainBlogId");


CREATE UNIQUE INDEX "IX_Posts_NameMapId" ON "Posts" ("NameMapId");


CREATE INDEX "IX_Posts_SubBlogId" ON "Posts" ("SubBlogId");



