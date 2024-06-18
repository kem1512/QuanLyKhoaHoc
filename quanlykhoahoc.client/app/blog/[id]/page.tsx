"use client";

import {
  Alert,
  Avatar,
  Card,
  Group,
  Image,
  Text,
  Title,
  TypographyStylesProvider,
} from "@mantine/core";
import RootLayout from "../../../components/Layout/RootLayout/RootLayout";
import useSWR from "swr";
import { BlogClient } from "../../web-api-client";
import { Comment } from "../../../components/Comment/Comment";
import Loading from "../../../components/Loading/Loading";
import Like from "../../../components/Like/Like";

export default function SingleBlog({ params }: { params: { id: number } }) {
  const { id } = params;

  const { data, isLoading, mutate } = useSWR(
    `/api/blog/${id}`,
    () => new BlogClient().getEntity(id),
    {
      revalidateOnReconnect: false,
      revalidateOnFocus: false,
    }
  );

  return (
    <RootLayout>
      {isLoading ? (
        <Loading />
      ) : data ? (
        <Card mt={"sm"}>
          <Card.Section mb={"sm"}>
            <Title order={3} mb={"sm"}>{data.title}</Title>
            <Group justify="space-between">
              <Group>
                <Avatar src={data.creator.avatar} />
                <div>
                  <Text>{data.creator.username}</Text>
                  <Text>{data.createTime.toDateString()}</Text>
                </div>
              </Group>
              <Like
                isLike={data.isLiked}
                comemntCount={data.numberOfComments}
                likeCount={data.numberOfLikes}
                blogId={id}
                mutate={mutate}
              />
            </Group>
          </Card.Section>
          <Card.Section mb={"sm"}>
            <Image src={data.image} radius={"lg"} alt={data.title} />
          </Card.Section>
          <Card.Section mb={"sm"}>
            <TypographyStylesProvider>
              <div
                dangerouslySetInnerHTML={{
                  __html: data.content,
                }}
              />
            </TypographyStylesProvider>
          </Card.Section>
          <Card.Section>
            <Comment blogId={id} />
          </Card.Section>
        </Card>
      ) : (
        <Alert>Không Có Gì Ở Đây Cả</Alert>
      )}
    </RootLayout>
  );
}
