import { PostCreatorFake } from "features/post-creator"
import PostsFeed from "features/posts-feed/ui"
import { useCallback, useEffect, useRef, useState } from "react"
import { useCookies } from "react-cookie"
import { Post } from "shared/models"

type appendPostFunction = {
	(post: Post): void
}

function ChatPage() {
	const ws = useRef<WebSocket | null>(null)
	const [cookies] = useCookies(["Token"])

	useEffect(() => {
		console.log("test")
		// var socket = new WebSocket("ws://javascript.ru/ws");
		console.log("test socket")
		ws.current = new WebSocket(
			`ws://${window.location.host}/api/chat/webSocket/?token=` +
				cookies.Token
		)
		ws.current.onopen = function (e) {
			console.log("on open")
			this.send(
				JSON.stringify({
					Type: "SendMessage",
					Payload: {
						TargetId: "cbf8e21f-4f17-43af-7ad0-08db5cab8373",
						Message: "Hello world",
					},
				})
			)
		}
		ws.current.onmessage = function (e) {
			console.log("message??")
			console.log(e.data)
		}
		ws.current.onclose = function (e) {
			console.log({ e })
			console.log("on close")
		}

		const wsCurrent = ws.current
		return () => {
			console.log("close!!!")
			wsCurrent.close()
		}
	}, [])

	return (
		<div className="Home">
			<h1 className="center-text">Chat</h1>
		</div>
	)
}

export default ChatPage
