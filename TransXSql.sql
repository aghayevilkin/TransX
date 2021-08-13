USE [TransXdb]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2f38d5cd-b302-47e0-8e9d-3eec363b652a', N'Moderator', N'MODERATOR', N'e47559b5-ea21-4720-8d86-15da8f81b062')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'98187799-a2e2-4186-8fc1-0f57c64fced6', N'Customer', N'CUSTOMER', N'58b65ba6-68d5-45f0-a9fe-4333c18dd580')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ae94c94c-0d0b-4bd0-8fd5-b76a5d48cc79', N'Admin', N'ADMIN', N'e1cbf1a3-7dd0-412d-b04f-f1f4baa88c53')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Discriminator], [Name], [Surname], [Image], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsVerify], [TwoFactorEnabled], [About], [Adress], [Profision], [IsTeam]) VALUES (N'402dedaf-b405-4f56-a121-33344d550b2d', N'CustomUser', N'Ilkin', N'Aghayev', N'f4254e57-6683-4a94-842c-f3317a643e54-16072021151428-gal_3.jpg', N'ilkinga@code.edu.az', N'ILKINGA@CODE.EDU.AZ', N'ilkinga@code.edu.az', N'ILKINGA@CODE.EDU.AZ', 1, N'AQAAAAEAACcQAAAAEHR8WAVIt2xkBMg+HOWjZcbiazdux7zS8/+WntGKkmwDPj2tkFcRdPP9xlzfD5DbVw==', N'TQ22WWOGINP54WYMXTQ2YRBPGOIJXYYA', N'96c6feb4-0555-477a-abcf-6cfcece75291', NULL, 0, NULL, 1, 0, 1, 0, NULL, N'Baku', N'Web Developer', 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'402dedaf-b405-4f56-a121-33344d550b2d', N'ae94c94c-0d0b-4bd0-8fd5-b76a5d48cc79')
GO
SET IDENTITY_INSERT [dbo].[Testimonials] ON 

INSERT [dbo].[Testimonials] ([Id], [Content], [Image], [AddedDate], [UserId]) VALUES (9, NULL, N'ae6b42e4-db2b-4824-bf79-c59d9fedc1e5-17072021054729-testimonials_img.png', CAST(N'2021-07-17T05:47:29.3161765' AS DateTime2), NULL)
INSERT [dbo].[Testimonials] ([Id], [Content], [Image], [AddedDate], [UserId]) VALUES (11, N'<p>&ldquo;Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga<br />
sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish<br />
luderick blackchin kingfish.</p>

<p>Midshipman, lightfish longfin smelt pickerel houndshark whiptail. Barracuda<br />
archerfish slimehead broadband dogfish, Pacific hake false trevally queen<br />
&nbsp;parrotfish Black prickleback blenny, bigeye squaretail nurseryfish<br />
yellowtail barracuda. Halibut: Blacksmelt&rdquo;</p>
', NULL, CAST(N'2021-07-17T06:20:40.1651228' AS DateTime2), N'402dedaf-b405-4f56-a121-33344d550b2d')
INSERT [dbo].[Testimonials] ([Id], [Content], [Image], [AddedDate], [UserId]) VALUES (13, N'<p>&ldquo;Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga<br />
sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish<br />
luderick blackchin kingfish.</p>

<p>Midshipman, lightfish longfin smelt pickerel houndshark whiptail. Barracuda<br />
archerfish slimehead broadband dogfish, Pacific hake false trevally queen<br />
&nbsp;parrotfish Black prickleback blenny, bigeye squaretail nurseryfish<br />
yellowtail barracuda. Halibut: Blacksmelt&rdquo;</p>
', NULL, CAST(N'2021-07-17T06:34:34.0719221' AS DateTime2), N'402dedaf-b405-4f56-a121-33344d550b2d')
SET IDENTITY_INSERT [dbo].[Testimonials] OFF
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'105687794642667005453', N'Google', N'402dedaf-b405-4f56-a121-33344d550b2d')
GO
SET IDENTITY_INSERT [dbo].[ServiceCategories] ON 

INSERT [dbo].[ServiceCategories] ([Id], [Name]) VALUES (1, N'Train')
INSERT [dbo].[ServiceCategories] ([Id], [Name]) VALUES (2, N'Truck')
INSERT [dbo].[ServiceCategories] ([Id], [Name]) VALUES (3, N'Online Tracking')
INSERT [dbo].[ServiceCategories] ([Id], [Name]) VALUES (5, N'Special Offers')
INSERT [dbo].[ServiceCategories] ([Id], [Name]) VALUES (6, N'Cargo Insurance')
INSERT [dbo].[ServiceCategories] ([Id], [Name]) VALUES (7, N'Box Checking')
SET IDENTITY_INSERT [dbo].[ServiceCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([Id], [Title], [Image], [Content], [PricePerKm], [CategoryId], [UserId], [AddedDate]) VALUES (3, N'Truck Freight', N'b595623f-a1df-48ed-b7fe-88edf72a67fa-23072021023829-vnl-25th-visionary-design.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish</p>

<p>North American darter albacore sand eel hoki cobbler European chub sand goby, marblefish. European eel p&iacute;ntano yellow moray ponyfish frilled shark. Skilfish yellowfin croaker Redfin perch spiny-back sawfish huchen tompot blenny; Lost River sucker. Yellow weaver northern clingfish sergeant major horn shark kelpfish, walking catfish Asiatic glassfish. Chubsucker; velvet-belly shark snailfish sailbearer wahoo moonfish blue danio, blue triggerfish slipmouth, &quot;spikefish pike eel bat ray.&quot; Kokopu, panga: ropefish seahorclown triggerfish hawkfish snake mackerel.</p>
', 13, 2, N'402dedaf-b405-4f56-a121-33344d550b2d', CAST(N'2021-07-21T15:48:35.7715451' AS DateTime2))
INSERT [dbo].[Services] ([Id], [Title], [Image], [Content], [PricePerKm], [CategoryId], [UserId], [AddedDate]) VALUES (4, N'Train Freight', N'2848b991-dd48-4ac1-9b92-4995357ac474-23072021005828-train.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish</p>

<p>North American darter albacore sand eel hoki cobbler European chub sand goby, marblefish. European eel p&iacute;ntano yellow moray ponyfish frilled shark. Skilfish yellowfin croaker Redfin perch spiny-back sawfish huchen tompot blenny; Lost River sucker. Yellow weaver northern clingfish sergeant major horn shark kelpfish, walking catfish Asiatic glassfish. Chubsucker; velvet-belly shark snailfish sailbearer wahoo moonfish blue danio, blue triggerfish slipmouth, &quot;spikefish pike eel bat ray.&quot; Kokopu, panga: ropefish seahorclown triggerfish hawkfish snake mackerel.</p>
', 10, 3, N'402dedaf-b405-4f56-a121-33344d550b2d', CAST(N'2021-07-23T00:47:32.5635313' AS DateTime2))
INSERT [dbo].[Services] ([Id], [Title], [Image], [Content], [PricePerKm], [CategoryId], [UserId], [AddedDate]) VALUES (5, N'Plane Freight', N'b0fb7ed2-4e90-47b4-8ddd-0c537781fbdc-23072021005953-service-details_img.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish</p>

<p>North American darter albacore sand eel hoki cobbler European chub sand goby, marblefish. European eel p&iacute;ntano yellow moray ponyfish frilled shark. Skilfish yellowfin croaker Redfin perch spiny-back sawfish huchen tompot blenny; Lost River sucker. Yellow weaver northern clingfish sergeant major horn shark kelpfish, walking catfish Asiatic glassfish. Chubsucker; velvet-belly shark snailfish sailbearer wahoo moonfish blue danio, blue triggerfish slipmouth, &quot;spikefish pike eel bat ray.&quot; Kokopu, panga: ropefish seahorclown triggerfish hawkfish snake mackerel.</p>
', 20, 6, N'402dedaf-b405-4f56-a121-33344d550b2d', CAST(N'2021-07-23T00:59:53.1151457' AS DateTime2))
INSERT [dbo].[Services] ([Id], [Title], [Image], [Content], [PricePerKm], [CategoryId], [UserId], [AddedDate]) VALUES (6, N'Ship Freight', N'a1ac2f13-1d16-4b28-b203-fa8512c350e9-23072021010301-ships.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish</p>

<p>North American darter albacore sand eel hoki cobbler European chub sand goby, marblefish. European eel p&iacute;ntano yellow moray ponyfish frilled shark. Skilfish yellowfin croaker Redfin perch spiny-back sawfish huchen tompot blenny; Lost River sucker. Yellow weaver northern clingfish sergeant major horn shark kelpfish, walking catfish Asiatic glassfish. Chubsucker; velvet-belly shark snailfish sailbearer wahoo moonfish blue danio, blue triggerfish slipmouth, &quot;spikefish pike eel bat ray.&quot; Kokopu, panga: ropefish seahorclown triggerfish hawkfish snake mackerel.</p>
', 28, 5, N'402dedaf-b405-4f56-a121-33344d550b2d', CAST(N'2021-07-23T01:03:01.7237437' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
SET IDENTITY_INSERT [dbo].[Socials] ON 

INSERT [dbo].[Socials] ([Id], [Icon], [Link]) VALUES (1, N'#facebook', N'https://facebook.com')
INSERT [dbo].[Socials] ([Id], [Icon], [Link]) VALUES (2, N'#twitter', N'https://twitter.com')
INSERT [dbo].[Socials] ([Id], [Icon], [Link]) VALUES (4, N'#linkedin', N'https://linkedin.com')
INSERT [dbo].[Socials] ([Id], [Icon], [Link]) VALUES (5, N'#instagram', N'https://instagram.com')
SET IDENTITY_INSERT [dbo].[Socials] OFF
GO
SET IDENTITY_INSERT [dbo].[SocialToUsers] ON 

INSERT [dbo].[SocialToUsers] ([Id], [Link], [SocialId], [UserId]) VALUES (2, N'https://www.facebook.com/agayeffilkin', 1, N'402dedaf-b405-4f56-a121-33344d550b2d')
INSERT [dbo].[SocialToUsers] ([Id], [Link], [SocialId], [UserId]) VALUES (3, N'https://twitter.com', 2, N'402dedaf-b405-4f56-a121-33344d550b2d')
INSERT [dbo].[SocialToUsers] ([Id], [Link], [SocialId], [UserId]) VALUES (6, N'https://linkedin.com', 4, N'402dedaf-b405-4f56-a121-33344d550b2d')
INSERT [dbo].[SocialToUsers] ([Id], [Link], [SocialId], [UserId]) VALUES (20, N'https://www.instagram.com/agayeffilkin/', 5, N'402dedaf-b405-4f56-a121-33344d550b2d')
SET IDENTITY_INSERT [dbo].[SocialToUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[BlogCategories] ON 

INSERT [dbo].[BlogCategories] ([Id], [Name]) VALUES (1, N'Warehouse')
INSERT [dbo].[BlogCategories] ([Id], [Name]) VALUES (2, N'BUSINESS')
INSERT [dbo].[BlogCategories] ([Id], [Name]) VALUES (1002, N'DELIVERY')
INSERT [dbo].[BlogCategories] ([Id], [Name]) VALUES (1003, N'ENVIRONMENT')
INSERT [dbo].[BlogCategories] ([Id], [Name]) VALUES (1004, N'TRANSPORT')
SET IDENTITY_INSERT [dbo].[BlogCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Blogs] ON 

INSERT [dbo].[Blogs] ([Id], [Title], [Image], [Content], [AddedDate], [CategoryId], [UserId], [BlogStatus]) VALUES (1024, N'Maintains positive momentum', N'3c752213-d5ec-4efe-a151-180edd996151-31072021031457-f2-blog_1.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<blockquote>
<p>Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish luderick blackchin.</p>
</blockquote>

<p><strong>Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok.</strong></p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish Pickerel dorado, p&iacute;ntano bonytail chub.</p>
', CAST(N'2021-07-31T03:13:55.0850125' AS DateTime2), 1, N'402dedaf-b405-4f56-a121-33344d550b2d', 1)
INSERT [dbo].[Blogs] ([Id], [Title], [Image], [Content], [AddedDate], [CategoryId], [UserId], [BlogStatus]) VALUES (1025, N'Acquires 120 new vehicles', N'94109c3d-65b9-4805-ac8e-758e9c57572e-31072021031550-f2-blog_2.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<blockquote>
<p>Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish luderick blackchin.</p>
</blockquote>

<p><strong>Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok.</strong></p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish Pickerel dorado, p&iacute;ntano bonytail chub.</p>
', CAST(N'2021-07-31T03:15:50.6958719' AS DateTime2), 2, N'402dedaf-b405-4f56-a121-33344d550b2d', 1)
INSERT [dbo].[Blogs] ([Id], [Title], [Image], [Content], [AddedDate], [CategoryId], [UserId], [BlogStatus]) VALUES (1026, N'Colgate-Palmolive renews', N'6ba05f7b-7b11-49a7-a127-069e5b663c1c-31072021031751-f2-blog_3.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<blockquote>
<p>Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish luderick blackchin.</p>
</blockquote>

<p><strong>Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok.</strong></p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish Pickerel dorado, p&iacute;ntano bonytail chub.</p>
', CAST(N'2021-07-31T03:17:51.6299261' AS DateTime2), 1002, N'402dedaf-b405-4f56-a121-33344d550b2d', 1)
INSERT [dbo].[Blogs] ([Id], [Title], [Image], [Content], [AddedDate], [CategoryId], [UserId], [BlogStatus]) VALUES (1027, N'Burts Potato Chips sizzle', N'18cb8498-7d42-4b8b-8168-eadda37fee6e-31072021032216-blog-detail.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<blockquote>
<p>Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish luderick blackchin.</p>
</blockquote>

<p><strong>Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok.</strong></p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish Pickerel dorado, p&iacute;ntano bonytail chub.</p>
', CAST(N'2021-07-31T03:22:16.3099309' AS DateTime2), 1002, N'402dedaf-b405-4f56-a121-33344d550b2d', 1)
INSERT [dbo].[Blogs] ([Id], [Title], [Image], [Content], [AddedDate], [CategoryId], [UserId], [BlogStatus]) VALUES (1029, N'Expands into beauty industry', N'25cc46ed-5fa4-4eb8-bc24-64d028a7062d-31072021032803-about_bg.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<blockquote>
<p>Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish luderick blackchin.</p>
</blockquote>

<p><strong>Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok.</strong></p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish Pickerel dorado, p&iacute;ntano bonytail chub.</p>
', CAST(N'2021-07-31T03:28:03.4905096' AS DateTime2), 1003, N'402dedaf-b405-4f56-a121-33344d550b2d', 1)
INSERT [dbo].[Blogs] ([Id], [Title], [Image], [Content], [AddedDate], [CategoryId], [UserId], [BlogStatus]) VALUES (1030, N'Expand Transporterium', N'af2b3710-bb1a-41cd-9d35-4403ddf4e97f-31072021033656-cases-slider_img.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<blockquote>
<p>Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish luderick blackchin.</p>
</blockquote>

<p><strong>Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok.</strong></p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish Pickerel dorado, p&iacute;ntano bonytail chub.</p>
', CAST(N'2021-07-31T03:36:56.1015966' AS DateTime2), 1004, N'402dedaf-b405-4f56-a121-33344d550b2d', 1)
INSERT [dbo].[Blogs] ([Id], [Title], [Image], [Content], [AddedDate], [CategoryId], [UserId], [BlogStatus]) VALUES (1031, N'Colgate-Palmolive renews', N'f9174d4c-39e6-4697-9830-e288f6b4ffa8-31072021034119-promo_2.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<blockquote>
<p>Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish luderick blackchin.</p>
</blockquote>

<p><strong>Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok.</strong></p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish Pickerel dorado, p&iacute;ntano bonytail chub.</p>
', CAST(N'2021-07-31T03:41:19.7505570' AS DateTime2), 1002, N'402dedaf-b405-4f56-a121-33344d550b2d', 1)
INSERT [dbo].[Blogs] ([Id], [Title], [Image], [Content], [AddedDate], [CategoryId], [UserId], [BlogStatus]) VALUES (1032, N'Colgate-Palmolive renews', N'09cbb1ce-69a4-41c2-a355-cadd9138be56-31072021035028-service-details_img.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<blockquote>
<p>Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish luderick blackchin.</p>
</blockquote>

<p><strong>Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok.</strong></p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish Pickerel dorado, p&iacute;ntano bonytail chub.</p>
', CAST(N'2021-07-31T03:50:28.9064773' AS DateTime2), 1004, N'402dedaf-b405-4f56-a121-33344d550b2d', 1)
INSERT [dbo].[Blogs] ([Id], [Title], [Image], [Content], [AddedDate], [CategoryId], [UserId], [BlogStatus]) VALUES (1033, N'Maintains positive momentum', N'447bacdb-127f-4c97-8a66-db928a9b7983-31072021035825-video-block.jpg', N'<p>Eelblenny ghost pipefish, cusk-eel red snapper horsefish, South American darter sailbearer electric stargazer. Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok leatherjacket Mexican golden trout cobia. Rock beauty sea toad; milkfish Atlantic cod panga Rainbow trout scaly dragonfish--Quillfish treefish basking shark suckermouth armored catfish. Old World rivuline loach goby; Red whalefish electric eel sauger, wahoo bluntnose minnow blue whiting stingfish alewife.</p>

<p>Sundaland noodlefish: man-of-war fish Blind shark shark bluntnose knifefish zingel perch pencilfish bobtail snipe eel. Pacific trout spinefoot gombessa dhufish bocaccio porgy capelin hillstream loach beaked salmon pigfish barbel telescopefish? Longfin dragonfish buri! Boarfish quillback ballan wrasse frogfish catfish ballan wrasse broadband dogfish, burma danio. Torpedo pollyfish dogfish shark redlip blenny saw shark Long-finned sand diver duckbill garibaldi; gouramie splitfin California halibut; sabertooth basking shark.</p>

<p>Grande perch speckled trout! Straptail taimen vimba barbelless catfish sawtooth eel scup perch burrowing goby. Siamese fighting fish Devario dogfish shark.</p>

<blockquote>
<p>Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga sailback scorpionfish sand dab, turkeyfish golden loach sand diver. Leopard danio p&iacute;ntano bonnetmouth; blue whiting, suckermouth armored catfish luderick blackchin.</p>
</blockquote>

<p><strong>Elephantnose fish stream catfish halosaur tripletail tilefish Russian sturgeon basslet weasel shark spotted danio. Crestfish stingray lenok.</strong></p>

<p>Moonfish triplespine crocodile shark pipefish yellowhead jawfish largemouth bass, bullhead shark Black scabbardfish smalltooth sawfish. Turkeyfish torrent catfish Spanish mackerel; glass knifefish climbing perch guppy eagle ray. Candiru, cuchia velvetfish Australian herring bleak salmon? Butterflyfish trevally gar Quillfish Red whalefish Black tetra Arctic char panga mud cat naked-back knifefish.&quot; Manefish sawfish burrowing goby loach climbing catfish eucla cod nurseryfish worm eel. Cowfish ling skipjack tuna. Noodlefish porbeagle shark carpsucker, snake eel silver hake grenadier, &quot;sand tiger southern sandfish loach minnow lake chub.&quot;Whiting grunion: sharksucker pigfish Pickerel dorado, p&iacute;ntano bonytail chub.</p>
', CAST(N'2021-07-31T03:58:25.3010638' AS DateTime2), 1, N'402dedaf-b405-4f56-a121-33344d550b2d', 1)
SET IDENTITY_INSERT [dbo].[Blogs] OFF
GO
SET IDENTITY_INSERT [dbo].[SavedBlogs] ON 

INSERT [dbo].[SavedBlogs] ([Id], [BlogId], [UserId], [AddedDate]) VALUES (71, 1032, N'402dedaf-b405-4f56-a121-33344d550b2d', CAST(N'2021-07-31T03:51:36.8740861' AS DateTime2))
INSERT [dbo].[SavedBlogs] ([Id], [BlogId], [UserId], [AddedDate]) VALUES (72, 1029, N'402dedaf-b405-4f56-a121-33344d550b2d', CAST(N'2021-07-31T03:59:01.8317670' AS DateTime2))
INSERT [dbo].[SavedBlogs] ([Id], [BlogId], [UserId], [AddedDate]) VALUES (73, 1030, N'402dedaf-b405-4f56-a121-33344d550b2d', CAST(N'2021-07-31T03:59:05.7906010' AS DateTime2))
INSERT [dbo].[SavedBlogs] ([Id], [BlogId], [UserId], [AddedDate]) VALUES (74, 1024, N'402dedaf-b405-4f56-a121-33344d550b2d', CAST(N'2021-07-31T03:59:09.4028858' AS DateTime2))
SET IDENTITY_INSERT [dbo].[SavedBlogs] OFF
GO
SET IDENTITY_INSERT [dbo].[RequestQuotes] ON 

INSERT [dbo].[RequestQuotes] ([Id], [From], [To], [When], [AddedDate], [ServiceId], [UserId]) VALUES (11, N'Gence', N'Bakiya', CAST(N'2021-09-14T08:00:00.0000000' AS DateTime2), CAST(N'2021-08-13T13:28:22.2052145' AS DateTime2), 3, N'402dedaf-b405-4f56-a121-33344d550b2d')
SET IDENTITY_INSERT [dbo].[RequestQuotes] OFF
GO
SET IDENTITY_INSERT [dbo].[Requests] ON 

INSERT [dbo].[Requests] ([Id], [Name], [Email], [DeliveryCity], [DepartureCity], [Weight], [Height], [Widhth], [Lenght], [InsuranceOrPackaging], [AddedDate], [ServiceId], [UserId]) VALUES (6, N'Ilkin', N'ilkinga@code.edu.az', N'Ağcabədi', N'Ağcabədi', 44, 43, 0, 0, N'Insurance', CAST(N'2021-07-27T06:54:17.8978384' AS DateTime2), 3, N'402dedaf-b405-4f56-a121-33344d550b2d')
INSERT [dbo].[Requests] ([Id], [Name], [Email], [DeliveryCity], [DepartureCity], [Weight], [Height], [Widhth], [Lenght], [InsuranceOrPackaging], [AddedDate], [ServiceId], [UserId]) VALUES (20, N'Ilkin', N'ilkinga@code.edu.az', N'Daşkəsən', N'Bakı', 55, 40, 60, 110, N'Packaging', CAST(N'2021-07-29T06:21:27.3596653' AS DateTime2), 6, N'402dedaf-b405-4f56-a121-33344d550b2d')
SET IDENTITY_INSERT [dbo].[Requests] OFF
GO
SET IDENTITY_INSERT [dbo].[Benefits] ON 

INSERT [dbo].[Benefits] ([Id], [Title], [Icon]) VALUES (1, N'Cargo Insurance', N'#insurance')
INSERT [dbo].[Benefits] ([Id], [Title], [Icon]) VALUES (3, N'Business analytic', N'#analitic')
INSERT [dbo].[Benefits] ([Id], [Title], [Icon]) VALUES (4, N'Box checking', N'#boxi')
SET IDENTITY_INSERT [dbo].[Benefits] OFF
GO
SET IDENTITY_INSERT [dbo].[BenefitsToServices] ON 

INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (76, 4, 1)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (77, 4, 3)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (78, 4, 4)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (79, 5, 1)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (80, 5, 3)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (81, 5, 4)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (84, 6, 1)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (85, 6, 3)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (86, 6, 4)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (100, 3, 1)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (101, 3, 3)
INSERT [dbo].[BenefitsToServices] ([Id], [ServiceId], [BenefitId]) VALUES (102, 3, 4)
SET IDENTITY_INSERT [dbo].[BenefitsToServices] OFF
GO
SET IDENTITY_INSERT [dbo].[IndustriesServeds] ON 

INSERT [dbo].[IndustriesServeds] ([Id], [Name]) VALUES (2, N'Consumer goods')
INSERT [dbo].[IndustriesServeds] ([Id], [Name]) VALUES (3, N'Food & Beverage')
INSERT [dbo].[IndustriesServeds] ([Id], [Name]) VALUES (4, N'Electronics')
SET IDENTITY_INSERT [dbo].[IndustriesServeds] OFF
GO
SET IDENTITY_INSERT [dbo].[IndustriesServedToServices] ON 

INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (63, 4, 2)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (64, 4, 3)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (65, 4, 4)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (66, 5, 2)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (67, 5, 3)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (68, 5, 4)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (72, 6, 2)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (73, 6, 3)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (74, 6, 4)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (88, 3, 2)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (89, 3, 3)
INSERT [dbo].[IndustriesServedToServices] ([Id], [ServiceId], [IndustriesServedId]) VALUES (90, 3, 4)
SET IDENTITY_INSERT [dbo].[IndustriesServedToServices] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceOffereds] ON 

INSERT [dbo].[ServiceOffereds] ([Id], [Name]) VALUES (2, N'Shared warehousing')
INSERT [dbo].[ServiceOffereds] ([Id], [Name]) VALUES (3, N'Crossdocking/Transloading')
INSERT [dbo].[ServiceOffereds] ([Id], [Name]) VALUES (4, N'FBA Preaparation')
SET IDENTITY_INSERT [dbo].[ServiceOffereds] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceOfferedToServices] ON 

INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (69, 4, 2)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (70, 4, 3)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (71, 4, 4)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (72, 5, 2)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (73, 5, 3)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (74, 5, 4)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (78, 6, 2)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (79, 6, 3)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (80, 6, 4)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (94, 3, 2)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (95, 3, 3)
INSERT [dbo].[ServiceOfferedToServices] ([Id], [ServiceId], [ServiceOfferedId]) VALUES (96, 3, 4)
SET IDENTITY_INSERT [dbo].[ServiceOfferedToServices] OFF
GO
SET IDENTITY_INSERT [dbo].[BlogTags] ON 

INSERT [dbo].[BlogTags] ([Id], [Name]) VALUES (1, N'Website')
INSERT [dbo].[BlogTags] ([Id], [Name]) VALUES (2, N'Network')
INSERT [dbo].[BlogTags] ([Id], [Name]) VALUES (3, N'Transport')
SET IDENTITY_INSERT [dbo].[BlogTags] OFF
GO
SET IDENTITY_INSERT [dbo].[TagToBlogs] ON 

INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1085, 1024, 1)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1086, 1024, 2)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1087, 1024, 3)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1090, 1025, 2)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1091, 1025, 3)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1094, 1026, 1)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1095, 1026, 2)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1102, 1027, 2)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1103, 1027, 3)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1111, 1029, 1)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1112, 1029, 2)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1113, 1029, 3)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1116, 1030, 1)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1117, 1030, 3)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1119, 1031, 2)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1123, 1032, 1)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1124, 1032, 2)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1125, 1032, 3)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1128, 1033, 1)
INSERT [dbo].[TagToBlogs] ([Id], [BlogId], [TagId]) VALUES (1129, 1033, 3)
SET IDENTITY_INSERT [dbo].[TagToBlogs] OFF
GO
SET IDENTITY_INSERT [dbo].[AboutMissions] ON 

INSERT [dbo].[AboutMissions] ([Id], [Title], [SubTitle], [Image]) VALUES (1, N'Our mission is to <span class="green-color">give you</span> good service', N'<strong>Trumpeter ropefish bonito roughy cobbler dogteeth tetra sturgeon pollock sea snail, saury woody sculpin southern sandfish blue.</strong> <br />  Tench South American darter bobtail snipe eel armored searobin lumpsucker combfish flat loach tang píntano spiderfish gunnel. Skilfish, halosaur skilfish manefish, bonnetmouth alfonsino largenose fish moonfish aruana glowlight danio. Basking shark halibut, North Pacific daggertooth, bonnetmouth sand stargazer sand goby. Queen triggerfish sand dab hammerhead shark zebra trout pelican gulper, common tunny boarfish. Pleco riffle dace flier trunkfish: North Pacific', N'c8629884-dd60-4233-82e8-30a5f6219841-17072021152722-section_1.jpg')
SET IDENTITY_INSERT [dbo].[AboutMissions] OFF
GO
SET IDENTITY_INSERT [dbo].[Abouts] ON 

INSERT [dbo].[Abouts] ([Id], [Title], [Content], [Image], [ImageTitle]) VALUES (1, N'Powerfull Features of <span class="green-color">Transporterium</span> company', N'<p>Brook trout powen harelip sucker gibberfish beluga sturgeon coelacanth tidewater goby elephant fish yellowtail slender snipe eel rasbora</p>

<p>Rainbowfish powen paddlefish brotula Arctic char zebra bullhead shark. Yellowhead jawfish gianttail temperate ocean-bass Atlantic eel river stingray skilfish. Flounder:</p>

<ul>
	<li>Red salmon skilfish, threadtail mullet southern flounder, orangespine unicorn fish flounder bobtail snipe eel</li>
	<li>Trumpeter ropefish bonito roughy cobbler dogteeth tetra Russian sturgeon pollock sea snail</li>
</ul>
', N'5efeb7a6-cc73-4858-945d-4c7c1b95db03-17072021155009-section_2.jpg', N'Faster than <br>you can imagine')
SET IDENTITY_INSERT [dbo].[Abouts] OFF
GO
SET IDENTITY_INSERT [dbo].[AboutServices] ON 

INSERT [dbo].[AboutServices] ([Id], [Title], [Icon]) VALUES (1, N'Warehouse', N'#warehouse')
INSERT [dbo].[AboutServices] ([Id], [Title], [Icon]) VALUES (2, N'Support 24/7', N'#support')
INSERT [dbo].[AboutServices] ([Id], [Title], [Icon]) VALUES (3, N'Online Tracking', N'#location')
INSERT [dbo].[AboutServices] ([Id], [Title], [Icon]) VALUES (4, N'Cargo Insurance', N'#insurance')
INSERT [dbo].[AboutServices] ([Id], [Title], [Icon]) VALUES (5, N'Worldwide', N'#worldwide')
INSERT [dbo].[AboutServices] ([Id], [Title], [Icon]) VALUES (6, N'Box checking', N'#boxi')
INSERT [dbo].[AboutServices] ([Id], [Title], [Icon]) VALUES (7, N'Always on time', N'#clock')
INSERT [dbo].[AboutServices] ([Id], [Title], [Icon]) VALUES (9, N'Business analytic', N'#analitic')
SET IDENTITY_INSERT [dbo].[AboutServices] OFF
GO
SET IDENTITY_INSERT [dbo].[AboutVideos] ON 

INSERT [dbo].[AboutVideos] ([Id], [VideoLink], [Title], [Image]) VALUES (1, N'https://www.youtube.com/watch?v=hSbVJ4y6I-8', N'Video presentation', N'2d29fcd1-176f-4300-8342-ea84bee4b976-18072021141502-video-block.jpg')
SET IDENTITY_INSERT [dbo].[AboutVideos] OFF
GO
SET IDENTITY_INSERT [dbo].[Achievements] ON 

INSERT [dbo].[Achievements] ([Id], [CountNum], [CountSub], [Title]) VALUES (1, 5, N'millions', N'Delivered packeges')
INSERT [dbo].[Achievements] ([Id], [CountNum], [CountSub], [Title]) VALUES (2, 2, N'million', N'Miles driven each of the year')
INSERT [dbo].[Achievements] ([Id], [CountNum], [CountSub], [Title]) VALUES (3, 50, N'years', N'Total warehouse area')
SET IDENTITY_INSERT [dbo].[Achievements] OFF
GO
SET IDENTITY_INSERT [dbo].[CaseStudies] ON 

INSERT [dbo].[CaseStudies] ([Id], [Image]) VALUES (1, N'0ba1395a-621b-40ab-aea8-56cf086e5db8-13082021113937-cases-slider_img.jpg')
INSERT [dbo].[CaseStudies] ([Id], [Image]) VALUES (3, N'0395ba79-434e-4b23-a25b-7e80978cf2aa-13082021114459-cases-slider_img-2.jpg')
INSERT [dbo].[CaseStudies] ([Id], [Image]) VALUES (4, N'99cbb5d9-16e5-4a8e-945a-3099b35bfdda-13082021114508-cases-slider_img-3.jpg')
INSERT [dbo].[CaseStudies] ([Id], [Image]) VALUES (5, N'8c74df77-21a0-4075-8084-93bc9ec40c1a-13082021114703-cases-slider_img-2.jpg')
SET IDENTITY_INSERT [dbo].[CaseStudies] OFF
GO
SET IDENTITY_INSERT [dbo].[Cities] ON 

INSERT [dbo].[Cities] ([Id], [Name]) VALUES (1, N'Ağcabədi')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (2, N'Ağdam')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (3, N'Ağdaş')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (4, N'Ağdərə')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (5, N'Ağstafa')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (6, N'Ağsu')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (7, N'Astara')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (8, N'Babək')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (9, N'Bakı')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (10, N'Balakən')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (11, N'Beyləqan')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (12, N'Bərdə')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (13, N'Biləsuvar')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (14, N'Cəbrayıl')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (15, N'Cəlilabad')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (16, N'Culfa')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (17, N'Daşkəsən')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (18, N'Dəliməmmədli')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (19, N'Xocalı')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (20, N'Füzuli')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (21, N'Gədəbəy')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (22, N'Gəncə')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (23, N'Goranboy')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (24, N'Göyçay')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (25, N'Göygöl')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (26, N'Göytəpə')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (27, N'Hacıqabul')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (28, N'Horadiz')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (29, N'Xaçmaz')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (30, N'Xankəndi')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (31, N'Xırdalan')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (32, N'Xızı')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (33, N'İmişli')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (34, N'İsmayıllı')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (35, N'Kəlbəcər')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (36, N'Kürdəmir')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (37, N'Qax')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (38, N'Qazax')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (39, N'Qəbələ')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (40, N'Qobustan')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (41, N'Quba')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (42, N'Qubadlı')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (43, N'Qusar')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (44, N'Laçın')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (45, N'Lerik')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (46, N'Lənkəran')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (47, N'Masallı')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (48, N'Mingəçevir')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (49, N'Naxçıvan')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (50, N'Neftçala')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (51, N'Oğuz')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (52, N'Ordubad')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (53, N'Saatlı')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (54, N'Sabirabad')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (55, N'Salyan')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (56, N'Samux')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (57, N'Siyəzən')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (58, N'Sumqayıt')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (59, N'Şabran')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (60, N'Şamaxı')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (61, N'Şəki')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (62, N'Şəmkir')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (63, N'Şərur')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (64, N'Şirvan')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (65, N'Şuşa')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (66, N'Tərtər')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (67, N'Tovuz')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (68, N'Ucar')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (69, N'Yardımlı')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (70, N'Yevlax')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (71, N'Zaqatala')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (72, N'Zəngilan')
INSERT [dbo].[Cities] ([Id], [Name]) VALUES (73, N'Zərdab')
SET IDENTITY_INSERT [dbo].[Cities] OFF
GO
SET IDENTITY_INSERT [dbo].[Faqs] ON 

INSERT [dbo].[Faqs] ([Id], [Title], [Subtitle], [IsQuestions]) VALUES (1, N'Construction management', N'Ronquil coho salmon red snapper duckbill Australian lungfish southern Dolly Varden? Black angelfish discus zebrafish thornyhead Antarctic cod; clownfish swordtail black dragonfish dogfish grunt sculpin European flounder. Flathead catfish sleeper shark, "piranha stoneroller minnow, flounder cownose ray dwarf loach.', 0)
INSERT [dbo].[Faqs] ([Id], [Title], [Subtitle], [IsQuestions]) VALUES (3, N'General contracting', N'Ronquil coho salmon red snapper duckbill Australian lungfish southern Dolly Varden? Black angelfish discus zebrafish thornyhead Antarctic cod; clownfish swordtail black dragonfish dogfish grunt sculpin European flounder. Flathead catfish sleeper shark, "piranha stoneroller minnow, flounder cownose ray dwarf loach.', 0)
INSERT [dbo].[Faqs] ([Id], [Title], [Subtitle], [IsQuestions]) VALUES (4, N'Verified professionals', N'Ronquil coho salmon red snapper duckbill Australian lungfish southern Dolly Varden? Black angelfish discus zebrafish thornyhead Antarctic cod; clownfish swordtail black dragonfish dogfish grunt sculpin European flounder. Flathead catfish sleeper shark, "piranha stoneroller minnow, flounder cownose ray dwarf loach.', 0)
INSERT [dbo].[Faqs] ([Id], [Title], [Subtitle], [IsQuestions]) VALUES (5, N'Powered by knowledge', N'Ronquil coho salmon red snapper duckbill Australian lungfish southern Dolly Varden? Black angelfish discus zebrafish thornyhead Antarctic cod; clownfish swordtail black dragonfish dogfish grunt sculpin European flounder. Flathead catfish sleeper shark, "piranha stoneroller minnow, flounder cownose ray dwarf loach.', 0)
INSERT [dbo].[Faqs] ([Id], [Title], [Subtitle], [IsQuestions]) VALUES (6, N'Construction management', N'Ronquil coho salmon red snapper duckbill Australian lungfish southern Dolly Varden? Black angelfish discus zebrafish thornyhead Antarctic cod; clownfish swordtail black dragonfish dogfish grunt sculpin European flounder. Flathead catfish sleeper shark, "piranha stoneroller minnow, flounder cownose ray dwarf loach.', 1)
INSERT [dbo].[Faqs] ([Id], [Title], [Subtitle], [IsQuestions]) VALUES (7, N'General contracting', N'Ronquil coho salmon red snapper duckbill Australian lungfish southern Dolly Varden? Black angelfish discus zebrafish thornyhead Antarctic cod; clownfish swordtail black dragonfish dogfish grunt sculpin European flounder. Flathead catfish sleeper shark, "piranha stoneroller minnow, flounder cownose ray dwarf loach.', 1)
INSERT [dbo].[Faqs] ([Id], [Title], [Subtitle], [IsQuestions]) VALUES (8, N'Verified professionals', N'Ronquil coho salmon red snapper duckbill Australian lungfish southern Dolly Varden? Black angelfish discus zebrafish thornyhead Antarctic cod; clownfish swordtail black dragonfish dogfish grunt sculpin European flounder. Flathead catfish sleeper shark, "piranha stoneroller minnow, flounder cownose ray dwarf loach.', 1)
INSERT [dbo].[Faqs] ([Id], [Title], [Subtitle], [IsQuestions]) VALUES (9, N'Powered by knowledge', N'Ronquil coho salmon red snapper duckbill Australian lungfish southern Dolly Varden? Black angelfish discus zebrafish thornyhead Antarctic cod; clownfish swordtail black dragonfish dogfish grunt sculpin European flounder. Flathead catfish sleeper shark, "piranha stoneroller minnow, flounder cownose ray dwarf loach.', 1)
SET IDENTITY_INSERT [dbo].[Faqs] OFF
GO
SET IDENTITY_INSERT [dbo].[Histories] ON 

INSERT [dbo].[Histories] ([Id], [Year], [Title], [SubTitle]) VALUES (1, 1993, N'Brook trout powen harelip sucker', N'Temperate ocean-bass, hussar Steve fish combtail gourami arrowtooth eel, blue whiting, Indian mul soapfish. Slickhead smelt catfish')
INSERT [dbo].[Histories] ([Id], [Year], [Title], [SubTitle]) VALUES (2, 1997, N'Rice eel jawfish roach longjaw mudsucker', N'Golden trout giant gourami Celebes rainbowfish straptail electric catfish zebrafish batfish starry flounder North American darter')
INSERT [dbo].[Histories] ([Id], [Year], [Title], [SubTitle]) VALUES (3, 2002, N'Longjaw mudsucker sawtooth eel', N'Pacific hake false trevally queen parrotfish Black pricklebackmosshead warbonnet sweeper! Greenling large-eye')
INSERT [dbo].[Histories] ([Id], [Year], [Title], [SubTitle]) VALUES (4, 2004, N'Ronquil coho salmon red snapper', N'Australian lungfish southern Dolly Varden? Black angelfish discus zebrafish thornyhead Antarctic cod; clownfish')
INSERT [dbo].[Histories] ([Id], [Year], [Title], [SubTitle]) VALUES (5, 1997, N'Rice eel jawfish roach longjaw mudsucker', N'Golden trout giant gourami Celebes rainbowfish straptail electric catfish zebrafish batfish starry flounder North American darter')
SET IDENTITY_INSERT [dbo].[Histories] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeAbouts] ON 

INSERT [dbo].[HomeAbouts] ([Id], [Title], [Content], [ExperianceTransportation], [SkilledDrivers], [MillesDrivenPerYear]) VALUES (2, N'The biggest Transportation Company in South region', N'<p><strong>Rockling Devario deep sea bonefish cutthroat trout streamer fish kaluga sailback scorpionfish sand dab, turkeyfish golden loach sand diver.</strong></p>

<p>Pacific hake false trevally queen parrotfish Black prickleback humuhumunukunukuapua&#39;a mosshead warbonnet sweeper! Greenling sleeper; Owens pupfish large-eye bream kokanee sprat shrimpfish grunter, Ratfish combtooth blenny, bigeye squaretail nurseryfish yellowtail barracuda. Halibut: king-of-the-salmon.</p>

<p>Pacific hake false trevally queen parrotfish Black prickleback humuhumunukunukuapua&#39;a mosshead warbonnet sweeper! Greenling sleeper; Owens pupfish large-eye bream kokanee sprat shrimpfish grunter.</p>

<p>Giant sea bass sailfish mooneye boga starry flounder? Longnose lancetfish coho salmon shiner, croaker, gouramie tui chub dragonfish freshwater herring? Flounder pearlfish kokanee duckbill eel Sacramento</p>
', 20, 500, 2)
SET IDENTITY_INSERT [dbo].[HomeAbouts] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeImages] ON 

INSERT [dbo].[HomeImages] ([Id], [Image]) VALUES (1, N'c2a7af44-518d-44de-8ba9-429b7165d877-13082021104216-map-transx.png')
SET IDENTITY_INSERT [dbo].[HomeImages] OFF
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([Id], [Name], [Email], [Subject], [Content], [AddedDate]) VALUES (30, N'Ilkin', N'ilkinga@code.edu.az', N'Lorem Ipsum is simply dummy text of the printing a', N'There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don''t look even slightly believable. If you are going to use a passage of Lorem Ipsum.', CAST(N'2021-07-26T13:48:18.2918581' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[PageHeaders] ON 

INSERT [dbo].[PageHeaders] ([Id], [Title], [Subtitle], [Image], [Page]) VALUES (7, N'Blog', N'TransX', N'7789bd12-1976-4ec8-9947-b2ba2aac699e-11072021135414-blog.jpg', N'blog')
INSERT [dbo].[PageHeaders] ([Id], [Title], [Subtitle], [Image], [Page]) VALUES (10, N'Blog Post', N'TransX', N'bb663181-b2d1-4ea5-ba96-34ac3041111a-11072021141730-blog-post_img.jpg', N'blogdetails')
INSERT [dbo].[PageHeaders] ([Id], [Title], [Subtitle], [Image], [Page]) VALUES (12, N'Contact us', N'TransX', N'7576752c-46af-480c-a424-de929be0d686-15072021154725-service-details.jpg', N'contact')
INSERT [dbo].[PageHeaders] ([Id], [Title], [Subtitle], [Image], [Page]) VALUES (14, N'About Us', N'TransX', N'2d1c9d45-e138-4847-8889-362f06ed3919-17072021133856-about_bg.jpg', N'about')
INSERT [dbo].[PageHeaders] ([Id], [Title], [Subtitle], [Image], [Page]) VALUES (15, N'Creative Team', N'TransX', N'5ad78183-2857-4235-9934-eaf12b68a3c6-19072021134136-accordion-bg.jpg', N'team')
INSERT [dbo].[PageHeaders] ([Id], [Title], [Subtitle], [Image], [Page]) VALUES (16, N'Services', N'TransX', N'6ac96ece-f262-4644-8dd7-cbfa5d49f98d-21072021111448-typography.jpg', N'service')
INSERT [dbo].[PageHeaders] ([Id], [Title], [Subtitle], [Image], [Page]) VALUES (17, N'Service Details', N'TransX', N'9c1c37e5-8fdc-4c94-96eb-515f815bc39a-21072021111538-service-details.jpg', N'servicedetails')
INSERT [dbo].[PageHeaders] ([Id], [Title], [Subtitle], [Image], [Page]) VALUES (18, N'F.A.G', N'TransX', N'ab41ad65-bef0-49e5-a491-43e23cf1e6cf-25072021235542-typography.jpg', N'faq')
INSERT [dbo].[PageHeaders] ([Id], [Title], [Subtitle], [Image], [Page]) VALUES (19, N'Request Quote', N'TransX', N'74f20e8c-ad82-4ecf-af5b-864e4a569b23-27072021143521-request-bg.jpg', N'request')
INSERT [dbo].[PageHeaders] ([Id], [Title], [Subtitle], [Image], [Page]) VALUES (20, N'Worldwide Shipping', N'Roach yellowfin cutthroat trout zebra pleco ocean sunfish                             temperate bass pikehead elephant fish. Long-finned char northern pike bluegill                             temperate', N'e5d11032-1f31-49eb-a1fd-928d31337057-13082021102236-promo_2.jpg', N'home')
SET IDENTITY_INSERT [dbo].[PageHeaders] OFF
GO
SET IDENTITY_INSERT [dbo].[Partners] ON 

INSERT [dbo].[Partners] ([Id], [Image], [AddedDate]) VALUES (1, N'0374b9a4-83fe-4f28-aa31-76b2baecf71c-17072021030633-logo_1.png', CAST(N'2021-07-17T03:06:33.9752684' AS DateTime2))
INSERT [dbo].[Partners] ([Id], [Image], [AddedDate]) VALUES (2, N'3bdbb412-c1ac-41f5-807c-ee1ca8996912-17072021031135-logo_2.png', CAST(N'2021-07-17T03:11:35.9594739' AS DateTime2))
INSERT [dbo].[Partners] ([Id], [Image], [AddedDate]) VALUES (6, N'11184436-6730-4bfd-935e-64f5cf045d0a-17072021032129-logo_3.png', CAST(N'2021-07-17T03:21:29.6403989' AS DateTime2))
INSERT [dbo].[Partners] ([Id], [Image], [AddedDate]) VALUES (8, N'63a27112-f85d-4e70-9972-c72248b1dcf6-17072021033710-logo_4.png', CAST(N'2021-07-17T03:37:10.0338866' AS DateTime2))
INSERT [dbo].[Partners] ([Id], [Image], [AddedDate]) VALUES (10, N'b8841090-ab96-446e-af91-2ff7049c5dc8-17072021033751-logo_2.png', CAST(N'2021-07-17T03:37:51.1083352' AS DateTime2))
INSERT [dbo].[Partners] ([Id], [Image], [AddedDate]) VALUES (11, N'9ddda1bf-8098-4ab9-ad35-f913decdfff6-17072021033801-logo_3.png', CAST(N'2021-07-17T03:38:01.3219812' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Partners] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceStepsforWorks] ON 

INSERT [dbo].[ServiceStepsforWorks] ([Id], [Title], [SubTitle]) VALUES (1, N'Listening you', N'Kokanee sprat shrimpfish. Pacific hake false trevally queen parrotfish Black prickleback')
INSERT [dbo].[ServiceStepsforWorks] ([Id], [Title], [SubTitle]) VALUES (2, N'Lane pairing analysis', N'Greenling sleeper; Owens pupfish large-eye bream kokanee sprat shrimpfish. Pacific hake false')
INSERT [dbo].[ServiceStepsforWorks] ([Id], [Title], [SubTitle]) VALUES (3, N'Check all details', N'Mosshead warbonnet sweeper! Greenling sleeper; Owens pupfish large-eye bream kokanee sprat')
INSERT [dbo].[ServiceStepsforWorks] ([Id], [Title], [SubTitle]) VALUES (4, N'Fast & efficient delivery', N'Greenling sleeper; Owens pupfish large-eye bream kokanee sprat shrimpfish. Pacific hake false')
SET IDENTITY_INSERT [dbo].[ServiceStepsforWorks] OFF
GO
SET IDENTITY_INSERT [dbo].[Settings] ON 

INSERT [dbo].[Settings] ([Id], [LogoWhite], [LogoBlack], [Location], [Phone], [Email], [OpenningHours], [Footer]) VALUES (17, N'12da218e-a876-4e09-9bcf-1bb2c2227582-12072021051319-logo_white.png', N'adf8ee52-fce4-4e4f-82c1-842a6faf295f-12072021051319-logo_black.png', N'Elliott Ave, Parkville VIC 3052, Melbourne', N'+31 85 964 47 20', N'info@transx.com', N'9:00 AM - 5:00 PM', N'Copyright All right reserved.')
SET IDENTITY_INSERT [dbo].[Settings] OFF
GO
SET IDENTITY_INSERT [dbo].[Subscribes] ON 

INSERT [dbo].[Subscribes] ([Id], [Email], [AddedDate]) VALUES (14, N'Ilkinga@code.edu.az', CAST(N'2021-07-27T14:45:01.0373054' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Subscribes] OFF
GO
SET IDENTITY_INSERT [dbo].[TeamImages] ON 

INSERT [dbo].[TeamImages] ([Id], [Image]) VALUES (1, N'e3d8cf65-73b5-41f1-a2e2-4999be8d258d-19072021140421-team.png')
SET IDENTITY_INSERT [dbo].[TeamImages] OFF
GO
SET IDENTITY_INSERT [dbo].[TransporteriumServices] ON 

INSERT [dbo].[TransporteriumServices] ([Id], [Title], [SubTitle], [Image]) VALUES (1, N'Road Freight', N'Kokanee sprat shrimpfish. Pacific hake false trevally queen parrotfish Black prickleback', N'70285986-d4b5-4442-b6ad-44fb05566358-23072021062307-road_freight.svg')
INSERT [dbo].[TransporteriumServices] ([Id], [Title], [SubTitle], [Image]) VALUES (2, N'Shipping Freight', N'Greenling sleeper; Owens pupfish large-eye bream kokanee sprat shrimpfish. Pacific hake false', N'9b6a1723-756b-4ce0-9865-d767eeb17044-23072021062342-shipping.svg')
INSERT [dbo].[TransporteriumServices] ([Id], [Title], [SubTitle], [Image]) VALUES (3, N'Air Freight', N'Mosshead warbonnet sweeper! Greenling sleeper; Owens pupfish large-eye bream kokanee sprat', N'2cc05e48-6b49-4430-a084-9b3e9619614f-23072021062416-plane.svg')
INSERT [dbo].[TransporteriumServices] ([Id], [Title], [SubTitle], [Image]) VALUES (4, N'Train Freight', N'Pacific hake false trevally queen parrotfish Black prickleback kokanee sprat', N'ff9a31d4-d829-4090-9439-4656d5fd1055-23072021062503-train.svg')
SET IDENTITY_INSERT [dbo].[TransporteriumServices] OFF
GO
