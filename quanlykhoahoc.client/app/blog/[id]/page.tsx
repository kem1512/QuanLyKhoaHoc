"use client";

import {
  Card,
  Group,
  Stack,
  Text,
  Title,
  TypographyStylesProvider,
} from "@mantine/core";
import RootLayout from "../../../components/Layout/RootLayout/RootLayout";
import useSWR from "swr";
import { BlogClient } from "../../web-api-client";
import { Comment } from "../../../components/Comment/Comment";

export default function SingleBlog({ params }: { params: { id: number } }) {
  const { id } = params;

  const { data } = useSWR(`/api/blog/${id}`, () =>
    new BlogClient().getEntity(id)
  );

  return (
    <RootLayout>
      <Card py={"xl"}>
        <Card.Section>
          <Group justify="space-between">
            <Title>{data?.title}</Title>
            <Stack>
              <Text>{data?.createTime?.toDateString()}</Text>
              <Text>{data?.creator?.username}</Text>
            </Stack>
          </Group>
        </Card.Section>
        <Card.Section my={"sm"}>
          <TypographyStylesProvider>
            <div
              dangerouslySetInnerHTML={{
                __html: data?.content,
              }}
            />
          </TypographyStylesProvider>
        </Card.Section>
        <Card.Section>
          <Comment blogId={id} />
        </Card.Section>
      </Card>
    </RootLayout>
  );
}
