"use client";

import {
  Text,
  Avatar,
  Group,
  TypographyStylesProvider,
  Paper,
  Stack,
  Menu,
  ActionIcon,
  Alert,
  Center,
} from "@mantine/core";
import classes from "./Answers.module.css";
import useSWR from "swr";
import { AnswersClient } from "../../app/web-api-client";
import { useQuery } from "../../lib/helper";
import { IconDots } from "@tabler/icons-react";
import Loading from "../Loading/Loading";
import AppPagination from "../AppPagination/AppPagination";
export function Answers({
  questionId,
  user,
  setAnswers,
  handleDelete,
}: {
  questionId: number;
  user: any;
  setAnswers: any;
  handleDelete: any;
  handleSubmit: any;
}) {
  const query = useQuery();

  const { data, isLoading } = useSWR(
    `/api/answers/${questionId}/${new URLSearchParams(query)}`,
    () =>
      new AnswersClient().getEntities(
        questionId,
        query.filters,
        query.sorts ?? "-CreateTime",
        query.page ? parseInt(query.page) : 1,
        query.pageSize ? parseInt(query.pageSize) : 5
      ),
    {
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  return (
    <Stack gap={"sm"}>
      {isLoading ? (
        <Loading />
      ) : data?.items?.length && data.items.length >= 0 ? (
        <>
          {data.items.map((item) => {
            return (
              <Paper
                key={item.id}
                withBorder
                radius="md"
                className={classes.comment}
                data-active={
                  item.id.toString() === query.commentId || undefined
                }
              >
                <Group justify="space-between">
                  <Avatar
                    src={item.user.avatar}
                    alt={item.user?.username}
                    radius="xl"
                  />
                  <Stack me={"auto"} gap={5}>
                    <Text fz="sm" fw={"bold"}>
                      {item.user?.username}
                    </Text>
                    <TypographyStylesProvider>
                      <div
                        style={{ fontSize: "var(--mantine-font-size-sm)" }}
                        dangerouslySetInnerHTML={{
                          __html: item.answer,
                        }}
                      />
                    </TypographyStylesProvider>
                  </Stack>
                  <Menu>
                    <Menu.Target>
                      <ActionIcon variant="transparent">
                        <IconDots width={20} />
                      </ActionIcon>
                    </Menu.Target>
                    <Menu.Dropdown>
                      {user?.email === item.user.email && (
                        <>
                          <Menu.Item onClick={() => setAnswers(item)}>
                            Sửa
                          </Menu.Item>
                          <Menu.Item onClick={() => handleDelete(item.id)}>
                            Xóa
                          </Menu.Item>
                        </>
                      )}
                      <Menu.Item
                        onClick={() =>
                          setAnswers((prev) => ({
                            ...prev,
                            parentId: item.id,
                          }))
                        }
                      >
                        Trả Lời
                      </Menu.Item>
                    </Menu.Dropdown>
                  </Menu>
                </Group>
              </Paper>
            );
          })}
          <Center>
            <AppPagination page={data?.pageNumber} total={data?.totalPages} />
          </Center>
        </>
      ) : (
        <Alert>Chưa Có Bình Luận</Alert>
      )}
    </Stack>
  );
}
