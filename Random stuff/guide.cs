(Stapje naar rechts is een . ertussen [bijv. Discogs.get.Master()])
(niks tussen haakjes is verzameling van properties)
(die user dingen doen niks want we hebben geen echte user)





Discogs
    Master (type)
        pagination
            page (int)
            pages (int)
            per_page (int)
            items (int)
            urls
                last (string)
                next (string)
        results[]
            title (string)
            country (string)
            year (string)
            format (string[])
            label (string[])
            type (string)
            id (int)
            barcode (string[])
            user_data
                in_wantlist (bool)
                in_collection (bool)
            master_id (int)
            master_url (string)
            uri (string)
            catno (string)
            thumb (string)
            cover_image (string)
            resource_url (string)
            community
                want (int)
                have (int)

    Release (type)
        pagination
            page (int)
            pages (int)
            per_page (int)
            items (int)
            urls
                last (string)
                next (string)
        filters
            applied
                format (string[])
            available
        filter_facets[]
            title (string)
            id (string)
            values[]
                title (string)
                value (string)
                count (int)
            allow_multiple_values (bool)
        versions[]
            id (int)
            label (string)
            country (string)
            title (string)
            major_formats (string[])
            format (string)
            catno (string)
            released (string)
            status (string)
            resource_url (string)
            thumb (string)
            stats
                community
                    in_wantlist (int)
                    in_collection (int)
                user
                    in_wantlist (int)
                    in_collection (int)

    ReleaseInfo
        id (int)
        status (string)
        year (int)
        resource_url (string)
        uri (string)
        artists[]
            namme (string)
            anv (string)
            join (string) //Als er een andere is staat hier het teken waarme zee samen gevoegd worden bijv /
            role (string)
            tracks (string)
            id (int)
            resource_url (string)
            thumbnail_url (string)
        artists_sort (string)
        labels[]
            name (string)
            catno (string)
            entity_type (string)
            entity_type_name (string)
            id (int)
            resource_url (string)
            thumbnail_url (string)
        //series[] --0 idee wat dit is het is tot nu toe altijd leeg, zit dus niet in de class
        companies[]
            name (string)
            catno (string)
            entity_type (string)
            entity_type_name (string)
            id (int)
            resource_url (string)
            thumbnail_url (string)
        formats[]
            name (string)
            qty (string)
            descriptions (string[])
            data_quality (string)
            community
                have (int)
                want (int)
                rating
                    count (int)
                    average (float)
                data_quality (string)
                status (string)
            format_quantity (int)
            

    get
        Masters(search [string], page [int], per_page [int]) (function, returns Master)
        Releases(master_id [int], page [int], per_page [int]) (function, returns Release)
        Image(urlImage [string]) (function, returns Texture2D)
        ImageList(urlImages [List<string>]) (function, returns List<Texture2D>)