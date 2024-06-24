import {
  Alert,
  AspectRatio,
  Button,
  Collapse,
  Grid,
  Stack,
  Tabs,
  rem,
} from "@mantine/core";
import React, { useEffect, useState } from "react";
import useSWR from "swr";
import {
  ISubjectDetailMapping,
  SubjectClient,
} from "../../../app/web-api-client";
import {
  IconArrowDown,
  IconArrowUp,
  IconCheck,
  IconMessageCircle,
  IconPhoto,
  IconSettings,
} from "@tabler/icons-react";
import Loading from "../../Loading/Loading";
import classes from "./StudyLayout.module.css";
import Link from "next/link";
import ReactPlayer from "react-player";
import { useSearchParams } from "next/navigation";
import { Question } from "../../Question/Question";

export default function StudyLayout({
  children,
  params,
  subjectDetailId,
}: {
  children: React.ReactNode;
  params?: { id: number };
  subjectDetailId: string;
}) {
  const { id } = params;

  const searchParams = useSearchParams();

  const subjectId = searchParams.get("subjectId");

  const [opened, setOpened] = useState<boolean[]>(new Array().fill(false));

  const { data, isLoading } = useSWR(`/api/subject/${id}`, () =>
    new SubjectClient().getEntities(id, true, null, null, null, null)
  );

  const [subjectDetail, setSubjectDetail] = useState<ISubjectDetailMapping>();

  useEffect(() => {
    document.body.style.overflow = "hidden";

    return () => {
      document.body.style.overflow = "auto";
    };
  }, []);

  return isLoading ? (
    <Loading />
  ) : data ? (
    <Grid p={"xs"}>
      <Grid.Col span={{ base: 12, lg: 9 }}>
        <Tabs variant="unstyled" defaultValue="practice" classNames={classes}>
          <Tabs.List grow>
            <Tabs.Tab
              value="practice"
              leftSection={
                <IconSettings style={{ width: rem(16), height: rem(16) }} />
              }
            >
              Bài Tập
            </Tabs.Tab>
            <Tabs.Tab
              value="video"
              leftSection={
                <IconMessageCircle
                  style={{ width: rem(16), height: rem(16) }}
                />
              }
            >
              Video Hướng Dẫn
            </Tabs.Tab>
            <Tabs.Tab
              value="question"
              leftSection={
                <IconPhoto style={{ width: rem(16), height: rem(16) }} />
              }
            >
              Hỏi Đáp
            </Tabs.Tab>
          </Tabs.List>
          <Tabs.Panel value="practice">{children}</Tabs.Panel>
          <Tabs.Panel value="video">
            <AspectRatio>
              {subjectId && subjectDetailId && (
                <ReactPlayer
                  url={
                    data.items
                      .find((c) => c.id.toString() == subjectId)
                      .subjectDetails.find(
                        (c) => c.id.toString() == subjectDetailId
                      ).linkVideo
                  }
                />
              )}
            </AspectRatio>
          </Tabs.Panel>
          <Tabs.Panel value="question">
            <Question subjectDetailId={Number(subjectDetailId)} />
          </Tabs.Panel>
        </Tabs>
      </Grid.Col>
      <Grid.Col
        span={{ base: 12, lg: 3 }}
        style={{
          overflow: "auto",
          height: "calc(100vh - var(--mantine-spacing-xs))",
        }}
      >
        <Stack gap={"sm"}>
          {data.items?.map((item, index) => {
            return (
              <>
                <Button
                  key={item.id}
                  onClick={() =>
                    setOpened((prev) => ({
                      ...prev,
                      [index]: !opened[index],
                    }))
                  }
                  justify="space-between"
                  disabled={item.learningProgresses.length > 0 ? false : true}
                  rightSection={
                    opened[index] ? (
                      <IconArrowUp width={18} height={18} />
                    ) : (
                      <IconArrowDown width={18} height={18} />
                    )
                  }
                >
                  {item.name}
                </Button>
                <Collapse in={opened[index]}>
                  <Stack gap={"sm"} py={"none"}>
                    {item.subjectDetails.map((subjectDetail) => (
                      <Link
                        href={`/study/${id}?subjectId=${item.id}&subjectDetailId=${subjectDetail.id}`}
                        onClick={() => setSubjectDetail(subjectDetail)}
                      >
                        <Button
                          rightSection={
                            subjectDetail.completed ? <IconCheck /> : undefined
                          }
                          color="grape"
                          variant={
                            subjectDetail.id.toString() === subjectDetailId
                              ? "filled"
                              : "outline"
                          }
                          justify="start"
                          w={"100%"}
                        >
                          {subjectDetail.name}
                        </Button>
                      </Link>
                    ))}
                  </Stack>
                </Collapse>
              </>
            );
          })}
        </Stack>
      </Grid.Col>
    </Grid>
  ) : (
    <Alert>Không Có Gì Ở Đây Cả</Alert>
  );
}
