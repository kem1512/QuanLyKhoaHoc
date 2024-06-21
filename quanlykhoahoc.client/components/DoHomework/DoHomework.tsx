import { Editor } from "@monaco-editor/react";
import { IPracticeMapping } from "../../app/web-api-client";
import { Button, Card, Text } from "@mantine/core";

export default function DoHomework({
  practice,
}: {
  practice: IPracticeMapping;
}) {
  return (
    <Card>
      <Card.Section p={"lg"}>
        <Text fw={"bold"}>Đề Bài</Text>
        <Text>{practice.title}</Text>
      </Card.Section>
      <Card.Section>
        <Editor
          height="75vh"
          defaultLanguage="javascript"
          options={{
            minimap: { enabled: false },
            overviewRulerBorder: false,
            wordWrap: "on",
          }}
          className="overflow-hidden"
        />
      </Card.Section>
      <Card.Section>
        <Button>Nộp Bài</Button>
      </Card.Section>
    </Card>
  );
}
