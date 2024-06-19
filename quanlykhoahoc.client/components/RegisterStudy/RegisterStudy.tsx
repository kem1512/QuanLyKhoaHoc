import {
  Accordion,
  AspectRatio,
  Grid,
  List,
  ListItem,
  Text,
} from "@mantine/core";
import {
  AccountClient,
  CourseSubject,
  ISubjectDetailMapping,
  PagingModelOfCourseSubjectMapping,
  RegisterStudyClient,
} from "../../app/web-api-client";
import React, { useEffect, useState } from "react";
import ReactPlayer from "react-player";
import useSWR from "swr";

export default function RegisterStudy({
  courseSubject,
  courseId,
}: {
  courseSubject: PagingModelOfCourseSubjectMapping;
  courseId: number;
}) {
  const [subjectDetail, setSubjectDetail] = useState<ISubjectDetailMapping>();

  const { data, isLoading, mutate } = useSWR(
    `/api/registerStudy/${courseId}`,
    () => new AccountClient().registerStudy(courseId)
  );

  useEffect(() => {
    if (courseSubject && data) {
      setSubjectDetail(
        courseSubject.items.find((c) => c.subjectId === data.currentSubjectId)
          .subject.subjectDetails[0]
      );
    }
  }, [data, courseSubject]);

  console.log(subjectDetail);

  return (
    <Grid>
      <Grid.Col span={{ base: 12, lg: 9 }}>
        {subjectDetail && (
          <AspectRatio ratio={16 / 9}>
            <ReactPlayer
              controls
              url={subjectDetail.linkVideo}
              width={"100%"}
              height={"100%"}
            />
          </AspectRatio>
        )}
      </Grid.Col>
      <Grid.Col span={{ base: 12, lg: 3 }}>
        <Text>{subjectDetail?.name}</Text>
        <Accordion chevronPosition="right" variant="contained">
          {courseSubject?.items?.map((item) => {
            return (
              <Accordion.Item
                value={item.subject.id.toString()}
                key={item.subject.id}
              >
                <Accordion.Control>
                  <Text>{item.subject.name}</Text>
                </Accordion.Control>
                <Accordion.Panel>
                  <List>
                    {item.subject.subjectDetails.map((subjectDetail) => (
                      <ListItem key={subjectDetail.id}>
                        <Text>{subjectDetail.name}</Text>
                      </ListItem>
                    ))}
                  </List>
                </Accordion.Panel>
              </Accordion.Item>
            );
          })}
        </Accordion>
      </Grid.Col>
    </Grid>
  );
}
