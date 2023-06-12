import { Box, Container, Stack, TextField } from "@mui/material"
import { UserAvatar } from "entities/user"
import { useEffect, useRef, useState } from "react"
import { IChatMessage, getLastMessages, useAuth, useChat } from "shared/api"
import s from "./index.module.css"

export default function ChatMessagesView({ targetId }: { targetId: string }) {
	const messageInputRef = useRef<HTMLInputElement>()
	const lastMessageElementRef = useRef<HTMLElement>()
	const [messages, setMessages] = useState<IChatMessage[]>([])
	const { user } = useAuth()
	const { chat } = useChat()

	const messagesRef = useRef(messages)
	useEffect(() => {
		messagesRef.current = messages
	}, [messages])

	const scrollToBottom = () => {
		lastMessageElementRef.current?.scrollIntoView({
			behavior: "smooth",
		})
		console.log({ da: lastMessageElementRef.current })
	}

	useEffect(() => {
		if (!chat) {
			return
		}

		const eventId = chat.on("message", (message: IChatMessage) => {
			setMessages([...messagesRef.current, message as IChatMessage])
			scrollToBottom()
		})
		return () => {
			chat.removeEventListener("messaage", eventId)
		}
	}, [chat])

	useEffect(() => {
		getLastMessages(targetId).then((data) => {
			setMessages(data.items.reverse())
			scrollToBottom()
		})
	}, [])

	const sendMessage = () => {
		const message = messageInputRef.current!.value
		chat?.sendMessage(targetId, message)
	}
	return (
		<div className={s.messages}>
			<Stack>
				<Container
					className={s.messagebox}
					sx={{
						height: "calc(100vh - 500px)",
						marginBottom: "10px",
						bgcolor: "#0e1621",
						padding: 2,
						borderRadius: 5
					}}
				>
					{messages.map((message) => (
						<Stack
							ref={lastMessageElementRef}
							key={message.id}
							marginTop={2}
							marginLeft={"8px"}
						>
							<Box
								className={
									user?.userId === message.ownerId
										? `${s.message} ${s.right}`
										: s.message
								}
							>
								<div className={s.avatar}>
									<UserAvatar
										user={message.ownerId}
										size={32}
									></UserAvatar>
								</div>
								{message.content}
							</Box>
						</Stack>
					))}
				</Container>
				<TextField
					placeholder="Send message"
					size="small"
					inputRef={messageInputRef}
					onKeyDown={(e) => {
						if (e.key === "Enter") sendMessage()
					}}
				/>
			</Stack>
		</div>
	)
}
