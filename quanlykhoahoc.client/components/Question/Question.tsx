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
  Button,
  Center,
} from "@mantine/core";
import classes from "./Question.module.css";
import useSWR from "swr";
import {
  MakeQuestionClient,
  MakeQuestionCreate,
  MakeQuestionUpdate,
  IMakeQuestionMapping,
  IAnswersMapping,
  AnswersClient,
  AnswersUpdate,
  AnswersCreate,
} from "../../app/web-api-client";
import { handleSubmit, useQuery } from "../../lib/helper";
import Editor from "../Editor/RichTextEditor/RichTextEditor";
import { useState } from "react";
import ActionButton from "../Helper/ActionButton";
import { IconDots } from "@tabler/icons-react";
import Loading from "../Loading/Loading";
import { modals } from "@mantine/modals";
import { useSelector } from "react-redux";
import AppPagination from "../AppPagination/AppPagination";
import { Answers } from "../Answers/Answers";

export function Question({ subjectDetailId }: { subjectDetailId: number }) {
  const [showAnswers, setShowAnswers] = useState<boolean[]>();

  const query = useQuery();

  const MakeQuestionService = new MakeQuestionClient();

  const AnswersService = new AnswersClient();

  const user = useSelector((state: any) => state.auth.user);

  const [answers, setAnswers] = useState<IAnswersMapping>({
    questionId: 0,
    answer: "",
  });

  const [makeQuestion, setMakeQuestion] = useState<IMakeQuestionMapping>({
    subjectDetailId: subjectDetailId,
    question: "",
  });

  const { data, mutate, isLoading } = useSWR(
    `/api/question/${subjectDetailId}/${new URLSearchParams(query)}`,
    () =>
      new MakeQuestionClient().getEntities(
        subjectDetailId,
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

  const handleDelete = (id: number | undefined) => {
    modals.openConfirmModal({
      title: "Xóa Bình Luận",
      children: (
        <Text size="sm">
          Bạn Chắc Chắn Muốn Xóa? Thao Tác Này Sẽ Không Thể Phục Hồi
        </Text>
      ),
      confirmProps: { color: "red" },
      labels: { confirm: "Chắc Chắn", cancel: "Hủy" },
      onConfirm: () =>
        handleSubmit(() => {
          return answers.question
            ? MakeQuestionService.deleteEntity(id)
            : AnswersService.deleteEntity(id);
        }, "Xóa Thành Công")
          .then(() => mutate())
          .then(() => mutate()),
    });
  };

  return (
    <Stack gap={"sm"}>
      <Editor
        content={answers.id ? answers.answer : makeQuestion.question}
        onChange={(e) =>
          answers.questionId
            ? setAnswers((prev) => ({ ...prev, answer: e }))
            : setMakeQuestion((prev) => ({ ...prev, question: e }))
        }
      />
      <Group gap={"xs"}>
        <ActionButton
          size="xs"
          action={() =>
            handleSubmit(
              () => {
                return (
                  answers.questionId
                    ? answers.id
                      ? AnswersService.updateEntity(
                          answers.id,
                          answers as AnswersUpdate
                        )
                      : AnswersService.createEntity(answers as AnswersCreate)
                    : makeQuestion.id
                    ? MakeQuestionService.updateEntity(
                        makeQuestion.id,
                        makeQuestion as MakeQuestionUpdate
                      )
                    : MakeQuestionService.createEntity(
                        makeQuestion as MakeQuestionCreate
                      )
                ).then(() => {
                  setMakeQuestion({
                    subjectDetailId,
                    question: "",
                  });
                  setAnswers({
                    questionId: 0,
                    answer: "",
                  });
                });
              },
              `${makeQuestion.id ? "Sửa" : "Thêm"} Thành Công`,
              mutate
            )
          }
        >
          Xác Nhận
        </ActionButton>
        {makeQuestion.id || answers.questionId || answers.id ? (
          <Button
            color="red"
            size="xs"
            onClick={() => {
              setMakeQuestion({
                subjectDetailId,
                question: "",
              });
              setAnswers({
                questionId: 0,
                answer: "",
              });
            }}
          >
            Hủy
          </Button>
        ) : null}
      </Group>
      {isLoading ? (
        <Loading />
      ) : data?.items?.length && data.items.length >= 0 ? (
        <>
          {data.items.map((item, index) => {
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
                          __html: item.question,
                        }}
                      />
                    </TypographyStylesProvider>
                    {item.numberOfAnswers > 0 && (
                      <>
                        <Text
                          fz={"sm"}
                          fw={"bold"}
                          style={{ cursor: "pointer" }}
                          size="xs"
                          onClick={() =>
                            setShowAnswers((prev) => ({
                              ...prev,
                              [index]: !showAnswers[index],
                            }))
                          }
                        >
                          Xem {item.numberOfAnswers} Câu Trả Lời
                        </Text>
                      </>
                    )}
                    {showAnswers && showAnswers[index] && (
                      <Answers
                        handleSubmit={handleSubmit}
                        questionId={item.id}
                        setAnswers={(e) => setAnswers(e)}
                        handleDelete={(e) => handleDelete(e)}
                        user={user}
                      />
                    )}
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
                          <Menu.Item onClick={() => setMakeQuestion(item)}>
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
                            questionId: item.id,
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
