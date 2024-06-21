import {
  AspectRatio,
  Button,
  Collapse,
  Grid,
  Group,
  RingProgress,
  Stack,
  Tabs,
  Text,
  rem,
} from "@mantine/core";
import {
  AccountClient,
  CourseSubjectClient,
  ISubjectDetailMapping,
  LearningProgressClient,
  PagingModelOfCourseSubjectMapping,
} from "../../app/web-api-client";
import React, { useEffect, useState } from "react";
import ReactPlayer from "react-player";
import useSWR from "swr";
import { Question } from "../Question/Question";
import Practice from "../Practice/Practice";
import { IconArrowDown, IconArrowLeft, IconArrowUp } from "@tabler/icons-react";
import Link from "next/link";

function CourseSubject({
  courseSubject,
  setSubjectDetail,
  currentSubjectId,
  registerStudyId,
}: {
  courseSubject: PagingModelOfCourseSubjectMapping;
  setSubjectDetail: any;
  currentSubjectId: number;
  registerStudyId: number;
}) {
  const { data } = useSWR(`/api/learningProgress/${registerStudyId}`, () =>
    new LearningProgressClient().getEntities(
      registerStudyId,
      null,
      null,
      null,
      null
    )
  );

  const [opened, setOpened] = useState<boolean[]>(new Array().fill(false));

  return (
    <Stack gap={"sm"}>
      {courseSubject?.items?.map((item, index) => {
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
              disabled={item.subjectId === currentSubjectId ? false : true}
              justify="space-between"
              rightSection={
                opened[index] ? (
                  <IconArrowUp width={18} height={18} />
                ) : (
                  <IconArrowDown width={18} height={18} />
                )
              }
            >
              {item.subject.name}
            </Button>
            <Collapse in={opened[index]}>
              <Stack gap={"sm"} py={"none"}>
                {item.subject.subjectDetails.map((subjectDetail) => (
                  <Button
                    variant="outline"
                    justify="start"
                    onClick={() => setSubjectDetail(subjectDetail)}
                  >
                    {subjectDetail.name}
                  </Button>
                ))}
              </Stack>
            </Collapse>
          </>
        );
      })}
    </Stack>
  );
}

export default function Study({ courseId }: { courseId: number }) {
  const [subjectDetail, setSubjectDetail] = useState<ISubjectDetailMapping>();

  const { data } = useSWR(
    `/api/registerStudy/${courseId}`,
    () => new AccountClient().registerStudy(courseId),
    {
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
      revalidateIfStale: false,
    }
  );

  const { data: courseSubject } = useSWR(`/api/courseSubject/${courseId}`, () =>
    new CourseSubjectClient().getEntities(courseId, null, null, null, null)
  );

  useEffect(() => {
    if (courseSubject && data) {
      setSubjectDetail(
        courseSubject.items.find((c) => c.subjectId === data.currentSubjectId)
          .subject.subjectDetails[0]
      );
    }
  }, [data, courseSubject]);

  useEffect(() => {
    document.body.style.overflow = "hidden";

    return () => {
      document.body.style.overflow = "auto";
    };
  }, []);

  return (
    <Tabs defaultValue="video">
      <Tabs.List>
        <Tabs.Tab value="home">
          <Link href={`/`}>
            <IconArrowLeft />
          </Link>
        </Tabs.Tab>
        <Tabs.Tab value="video">Video</Tabs.Tab>
        <Tabs.Tab value="practice">Bài Tập</Tabs.Tab>
        <Tabs.Tab value="question">Hỏi Đáp</Tabs.Tab>
        <Tabs.Tab value="current" fw={"bold"} ml="auto" p={0} disabled>
          <Group>
            <Text size="sm" fw={"bold"}>
              {subjectDetail?.name}
            </Text>
            <RingProgress
              size={45}
              thickness={3}
              sections={[{ value: 99, color: "blue" }]}
              label={
                <Text c="blue" ta="center" size={rem(10)}>
                  99%
                </Text>
              }
            />
          </Group>
        </Tabs.Tab>
      </Tabs.List>

      {subjectDetail && (
        <Grid p={"sm"}>
          <Grid.Col span={{ base: 12, lg: 9 }}>
            <Tabs.Panel value="video">
              <AspectRatio ratio={16 / 9}>
                <ReactPlayer
                  controls
                  url={subjectDetail.linkVideo}
                  width={"100%"}
                  height={"100%"}
                />
              </AspectRatio>
            </Tabs.Panel>

            <Tabs.Panel value="practice">
              <Practice subjectDetailId={subjectDetail.id} />
            </Tabs.Panel>

            <Tabs.Panel value="question">
              <Question subjectDetailId={subjectDetail.id} />
            </Tabs.Panel>
          </Grid.Col>
          <Grid.Col
            span={{ base: 12, lg: 3 }}
            pos={"relative"}
            style={{ overflow: "auto" }}
            h={"90vh"}
          >
            <div style={{ position: "absolute", inset: 0 }}>
              <CourseSubject
                registerStudyId={data.id}
                currentSubjectId={data.currentSubjectId}
                courseSubject={courseSubject}
                setSubjectDetail={setSubjectDetail}
              />
            </div>
          </Grid.Col>
        </Grid>
      )}
    </Tabs>
  );
}
