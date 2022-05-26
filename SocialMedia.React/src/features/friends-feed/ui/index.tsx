import { CircularProgress, Grid } from "@mui/material";
import { useCallback, useEffect, useRef, useState } from "react";
import { Filter, PaginatedRequest, PaginatedResult, User } from "shared/models";
import { getFriends } from "shared/api/friends";
import UserCard from "entities/user/ui/user-card";

export default function FriendsFeed({ userId }: { userId: string }) {
    const [friends, setFriends] = useState<User[]>([]);
    const [loading, setLoading] = useState<boolean>();

    const [page, setPage] = useState<number | undefined>();
    const defaultPageRequest = useRef<PaginatedRequest>({
        sortKey: "firstName",
        sortDirection: "DESC",
        pageSize: 3,
        filters: [],
        offset: new Date()
    });

    const [hasMore, setHasMore] = useState<boolean>(true);

    const nextPageTriggerRef = useRef<HTMLDivElement>(null);
    const observer = useRef<IntersectionObserver>();


    const handlePaginationResponse = useCallback((response: PaginatedResult<User>) => {
        if (response.items.length === 0) {
            setHasMore(false)
            return
        }

        setFriends([...friends, ...response.items]);
        setLoading(false)
    }, [friends]);

    const buildFilters = useCallback(() => {
        const filters: Filter[] = [];

        /* if (searchText) {
            filters.push({
                path: "firstName",
                value: searchText,
            })
        } */

        return filters;
    }, [])


    useEffect(() => {
        if (loading || !hasMore) {
            return;
        }

        observer.current = new IntersectionObserver(entries => {
            if (entries[0].isIntersecting && !loading && hasMore) {
                setLoading(true);

                const nextPage = page === undefined ? 0 : page + 1;
                setPage(nextPage)

                const request = {
                    ...defaultPageRequest.current,
                    page: nextPage
                }

                getFriends(userId, request).then(handlePaginationResponse)
            }
        })

        if (nextPageTriggerRef.current) {
            observer.current.observe(nextPageTriggerRef.current);
        }

        return () => observer.current?.disconnect();
    }, [loading, hasMore, handlePaginationResponse, nextPageTriggerRef, friends, buildFilters, page, userId])


    const clearPage = useCallback(() => {
        setPage(undefined)

        setLoading(false)
        setHasMore(true)
        setFriends([])
    }, [])
    useEffect(clearPage, [clearPage])

    return (
        <div className="feed">
            <Grid
                container
                spacing={1}
                direction="row"
                justifyContent="center"
                alignItems="center"
                alignContent="center"
                wrap="wrap"
            >
                {
                    friends.map((friend) => {
                        return (
                            <Grid key={friend.userId} item xs={4}>
                                {/* <PostCard post={post} key={post.id} /> */}
                                {/* {friend.firstName} {friend.lastName} */}
                                <UserCard user={friend} />
                            </Grid>
                        )
                    })
                }
            </Grid>

            <div ref={nextPageTriggerRef}></div>

            {
                hasMore ?? <div className="center-text"> <p><CircularProgress className="center-content" /></p> </div>
            }
        </div>
    );
}