import { Paper } from "@mui/material";
import CommentCard from "entities/comment/ui";
import CommentCreator from "features/comment-creator/ui";
import { useCallback, useEffect, useRef, useState } from "react";
import { getFeed } from "shared/api/comment";
import { Comment, Filter, PaginatedRequest, PaginatedResult, Post } from "shared/models";
import './index.css';

type appendFunction = {
    (post: Post): void
}

export default function CommentsFeed({ post }: { post: Post }) {
    const [comments, setComments] = useState<Comment[]>([]);
    const [loading, setLoading] = useState<boolean>();
    const [pageData, setPageData] = useState<PaginatedRequest>({
        sortKey: "createdAt",
        sortDirection: "asc",
        pageSize: 2
    });
    const [total, setTotal] = useState<number | undefined>();


    const handlePaginationResponse = (response: PaginatedResult<Comment>) => {
        setTotal(response.total);
        if (response.items.length === 0) {
            return
        }

        setPageData({
            ...pageData,
            offset: response.offset
        });
        setComments([...comments, ...response.items]);
        setLoading(false)
    };


    const fetchComments = (pageSize: number) => {
        if (!loading && (total === undefined || comments.length < total)) {
            setLoading(true);

            pageData.page = pageData.page === undefined ? 0 : pageData.page + 1;
            pageData.pageSize = pageSize;
            setPageData(pageData)

            getFeed(post.id, pageData).then(handlePaginationResponse)
        }
    };


    useEffect(() => {
        setLoading(false)
        fetchComments(2);
    }, [post])

    const onCommentCreated = useCallback((comment: Comment) => {
        setComments([comment, ...comments])
        if(total){
            setTotal(total + 1)
        }
    }, [comments])

    return (
        <div className="comments-feed">
            <CommentCreator postId={post.id} onCommentCreated={onCommentCreated} />
            {
                comments.map((comment) => {
                    return (
                        <CommentCard key={comment.id} comment={comment} />
                    )
                })
            }

            <div className="fetch-button" onClick={() => fetchComments(2)} hidden={total !== undefined && total <= comments.length}>
                Fetch more ({total! - comments.length})
            </div>
        </div>
    );
}