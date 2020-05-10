CREATE TABLE [dbo].[Sizes]
(
	[SizeId] [int] IDENTITY(1,1) NOT NULL,
	[SizeName] [varchar](50) NOT NULL,
	[SizeAbreviature] [varchar](10) NOT NULL,
	[StatusId] [bit] NULL,
	CONSTRAINT [PK_Sizes_SizeId] PRIMARY KEY CLUSTERED ([SizeId])
)
